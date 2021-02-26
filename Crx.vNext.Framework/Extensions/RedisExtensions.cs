using Crx.vNext.Common.Helper;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StackExchange.Redis;
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
                var redisConfig = ConfigurationOptions.Parse(Appsettings.GetString(new[] { "Redis", "Connection" }));
                redisConfig.ClientName = Appsettings.GetString(new[] { "Redis", "ClientName" });
                try
                {
                    services.AddSingleton((await ConnectionMultiplexer.ConnectAsync(redisConfig)).GetDatabase());
                }
                catch (System.Exception ex)
                {
                    Log.Error(ex, "Redis 连接异常！");
                    throw;
                }
            }
        }
    }
}
