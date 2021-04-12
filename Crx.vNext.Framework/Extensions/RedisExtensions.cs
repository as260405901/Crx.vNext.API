using Crx.vNext.Common.Helper;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StackExchange.Redis;
using System;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Crx.vNext.Framework.Extensions
{
    /// <summary>
    /// Redis配置
    /// </summary>
    public static class RedisExtensions
    {
        public static async Task AddRedisSetup(this IServiceCollection services)
        {
            if (Appsettings.GetBool(new[] { "Redis", "Enabled" }) ?? false)
            {
                var configuration = ConfigurationOptions.Parse(Appsettings.GetString(new[] { "Redis", "Connection" }));
                configuration.ClientName = Appsettings.GetString(new[] { "Redis", "ClientName" });
                configuration.SocketManager = new SocketManager("Crx.vNext.SocketManager", 20, false);
                // 微软推荐配置
                configuration.AbortOnConnectFail = false;
                configuration.AsyncTimeout = 15000;
                configuration.ConnectTimeout = 15000;
                configuration.SyncTimeout = 15000;
                if (Appsettings.GetBool(new[] { "Redis", "Ssl" }) ?? false)
                {
                    configuration.Ssl = true;
                    configuration.SslProtocols = (SslProtocols)Enum.Parse(typeof(SslProtocols), Appsettings.GetString(new[] { "Redis", "SslProtocols" }));
                }
                try
                {
                    services.AddSingleton((await ConnectionMultiplexer.ConnectAsync(configuration)).GetDatabase());
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Redis 连接异常！");
                    throw;
                }
            }
        }
    }
}
