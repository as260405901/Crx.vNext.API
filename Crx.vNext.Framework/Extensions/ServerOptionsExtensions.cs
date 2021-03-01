using Crx.vNext.Common.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Crx.vNext.Framework.Extensions
{
    /// <summary>
    /// 服务器配置
    /// </summary>
    public static class ServerOptionsExtensions
    {
        public static void AddServerOptionsSetup(this IServiceCollection services)
        {
            // 是否开启同步
            if (Appsettings.GetBool(new[] { "SystemFrame", "EnabledSync" }) ?? false)
            {
                services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                        .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);
            }
        }
    }
}
