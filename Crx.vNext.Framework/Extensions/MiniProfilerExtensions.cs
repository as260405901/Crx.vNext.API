using Crx.vNext.Common.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Crx.vNext.Framework.Extensions
{
    /// <summary>
    /// MiniProfiler配置
    /// </summary>
    public static class MiniProfilerExtensions
    {
        public static void AddMiniProfilerSetup(this IServiceCollection services)
        {
            if (Appsettings.GetBool(new[] { "SystemFrame", "EnabledMiniProfiler" }) ?? false)
            {
                services.AddMiniProfiler();
            }
        }
        public static void UseMiniProfilerSetup(this IApplicationBuilder app)
        {
            if (Appsettings.GetBool(new[] { "SystemFrame", "EnabledMiniProfiler" }) ?? false)
            {
                app.UseMiniProfiler();
            }
        }
    }
}
