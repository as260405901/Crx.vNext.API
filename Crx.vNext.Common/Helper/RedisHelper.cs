using StackExchange.Redis;
using System;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace Crx.vNext.Common.Helper
{
    /// <summary>
    /// Redis 初始化
    /// </summary>
    public static class RedisHelper
    {
        #region Redis 初始化
        private static Lazy<ConnectionMultiplexer> lazyConnection = CreateConnection();
        /// <summary>
        /// 线程安全，仅初始化单个已连接的 ConnectionMultiplexer 实例
        /// </summary>
        public static ConnectionMultiplexer Connection => lazyConnection.Value;

        public static ConfigurationOptions Configuration
        {
            get
            {
                var configuration = ConfigurationOptions.Parse(Appsettings.GetString(new[] { "Redis", "Connection" }));
                configuration.ClientName = Appsettings.GetString(new[] { "Redis", "ClientName" });
                configuration.SocketManager = new SocketManager("Crx.vNext.SocketManager",
                    Appsettings.GetInt(new[] { "Redis", "WorkerCount" }) ?? 20, false);
                // 微软推荐配置
                configuration.AbortOnConnectFail = false;
                configuration.AsyncTimeout = 15000;
                configuration.ConnectTimeout = 15000;
                configuration.SyncTimeout = 15000;
                if (Appsettings.GetBool(new[] { "Redis", "Ssl" }) ?? false)
                {
                    configuration.Ssl = true;
                    configuration.SslProtocols = (SslProtocols)Enum.Parse(typeof(SslProtocols),
                        Appsettings.GetString(new[] { "Redis", "SslProtocols" }));
                }
                return configuration;
            }
        }

        private static Lazy<ConnectionMultiplexer> CreateConnection() =>
            new Lazy<ConnectionMultiplexer>(() =>
            {                
                return ConnectionMultiplexer.Connect(Configuration);
            });

        public static IDatabase GetDatabase() => BasicRetry(() => Connection.GetDatabase());

        public static System.Net.EndPoint[] GetEndPoints() => BasicRetry(() => Connection.GetEndPoints());

        public static IServer GetServer(string host, int port) => BasicRetry(() => Connection.GetServer(host, port));
        #endregion

        #region Redis 重连
        private static long lastReconnectTicks = DateTimeOffset.MinValue.UtcTicks;
        private static DateTimeOffset firstErrorTime = DateTimeOffset.MinValue;
        private static DateTimeOffset previousErrorTime = DateTimeOffset.MinValue;

        private static readonly object reconnectLock = new object();

        // In general, let StackExchange.Redis handle most reconnects,
        // so limit the frequency of how often ForceReconnect() will
        // actually reconnect.
        public static TimeSpan ReconnectMinFrequency => TimeSpan.FromSeconds(60);

        // If errors continue for longer than the below threshold, then the
        // multiplexer seems to not be reconnecting, so ForceReconnect() will
        // re-create the multiplexer.
        public static TimeSpan ReconnectErrorThreshold => TimeSpan.FromSeconds(30);

        public static int RetryMaxAttempts => 5;


        // In real applications, consider using a framework such as
        // Polly to make it easier to customize the retry approach.
        private static T BasicRetry<T>(Func<T> func)
        {
            int reconnectRetry = 0;
            int disposedRetry = 0;

            while (true)
            {
                try
                {
                    return func();
                }
                catch (Exception ex) when (ex is RedisConnectionException || ex is SocketException)
                {
                    reconnectRetry++;
                    if (reconnectRetry > RetryMaxAttempts)
                        throw;
                    ForceReconnect();
                }
                catch (ObjectDisposedException)
                {
                    disposedRetry++;
                    if (disposedRetry > RetryMaxAttempts)
                        throw;
                }
            }
        }

        /// <summary>
        /// Force a new ConnectionMultiplexer to be created.
        /// NOTES:
        ///     1. Users of the ConnectionMultiplexer MUST handle ObjectDisposedExceptions, which can now happen as a result of calling ForceReconnect().
        ///     2. Don't call ForceReconnect for Timeouts, just for RedisConnectionExceptions or SocketExceptions.
        ///     3. Call this method every time you see a connection exception. The code will:
        ///         a. wait to reconnect for at least the "ReconnectErrorThreshold" time of repeated errors before actually reconnecting
        ///         b. not reconnect more frequently than configured in "ReconnectMinFrequency"
        /// </summary>
        public static void ForceReconnect()
        {
            var utcNow = DateTimeOffset.UtcNow;
            long previousTicks = Interlocked.Read(ref lastReconnectTicks);
            var previousReconnectTime = new DateTimeOffset(previousTicks, TimeSpan.Zero);
            TimeSpan elapsedSinceLastReconnect = utcNow - previousReconnectTime;

            // If multiple threads call ForceReconnect at the same time, we only want to honor one of them.
            if (elapsedSinceLastReconnect < ReconnectMinFrequency)
                return;

            lock (reconnectLock)
            {
                utcNow = DateTimeOffset.UtcNow;
                elapsedSinceLastReconnect = utcNow - previousReconnectTime;

                if (firstErrorTime == DateTimeOffset.MinValue)
                {
                    // We haven't seen an error since last reconnect, so set initial values.
                    firstErrorTime = utcNow;
                    previousErrorTime = utcNow;
                    return;
                }

                if (elapsedSinceLastReconnect < ReconnectMinFrequency)
                    return; // Some other thread made it through the check and the lock, so nothing to do.

                TimeSpan elapsedSinceFirstError = utcNow - firstErrorTime;
                TimeSpan elapsedSinceMostRecentError = utcNow - previousErrorTime;

                bool shouldReconnect =
                    elapsedSinceFirstError >= ReconnectErrorThreshold // Make sure we gave the multiplexer enough time to reconnect on its own if it could.
                    && elapsedSinceMostRecentError <= ReconnectErrorThreshold; // Make sure we aren't working on stale data (e.g. if there was a gap in errors, don't reconnect yet).

                // Update the previousErrorTime timestamp to be now (e.g. this reconnect request).
                previousErrorTime = utcNow;

                if (!shouldReconnect)
                    return;

                firstErrorTime = DateTimeOffset.MinValue;
                previousErrorTime = DateTimeOffset.MinValue;

                Lazy<ConnectionMultiplexer> oldConnection = lazyConnection;
                CloseConnection(oldConnection);
                lazyConnection = CreateConnection();
                Interlocked.Exchange(ref lastReconnectTicks, utcNow.UtcTicks);
            }
        }

        private static void CloseConnection(Lazy<ConnectionMultiplexer> oldConnection)
        {
            if (oldConnection == null)
                return;

            try
            {
                oldConnection.Value.Close();
            }
            catch (Exception)
            {
                // Example error condition: if accessing oldConnection.Value causes a connection attempt and that fails.
            }
        }
        #endregion

        #region Redis 扩展方法
        /// <summary>
        /// 缓存为空时执行func方法存至缓存，否则从缓存读取
        /// 使用方法： redis.Get(key, () => "123", 1);
        /// </summary>
        /// <param name="func">原始数据</param>
        public static async Task<T> Get<T>(this IDatabase redis, string key, Func<T> func = null, TimeSpan? expiry = null)
        {
            T obj = default;
            if (await redis.KeyExistsAsync(key))
            {
                obj = JsonHelper.Deserialize<T>(await redis.StringGetAsync(key));
            }
            else if (func != null)
            {
                obj = func();
                await redis.StringSetAsync(key, JsonHelper.Serialize(obj), expiry);
            }
            return obj;
        }
        #endregion
    }
}
