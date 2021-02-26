using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Crx.vNext.Framework.Extensions
{
    /// <summary>
    /// Serilog配置
    /// </summary>
    public static class SerilogExtensions
    {
        public static void AddSerilogSetup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        }


        public static void UseSerilogSetup(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(); // 需要详细日志时，将 Microsoft 设置为 Information
        }
    }
}
