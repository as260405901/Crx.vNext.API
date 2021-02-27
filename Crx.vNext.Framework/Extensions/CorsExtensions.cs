using Crx.vNext.Common.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Crx.vNext.Framework.Extensions
{
    /// <summary>
    /// 跨域设置
    /// </summary>
    public static class CorsExtensions
    {
        public static void AddCorsSetup(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("Crx.vNext.Cors",
                policy =>
                {
                    // 跨站IP请求
                    if (Appsettings.GetBool(new string[] { "SystemFrame", "Cors", "EnableAllIPs" }) ?? false)
                    {
                        policy.AllowAnyOrigin();
                    }
                    else
                    {
                        policy.WithOrigins(Appsettings.GetList<string>(new string[] { "SystemFrame", "Cors", "IPs" }).ToArray());
                    }
                    // 允许请求方式
                    var methods = Appsettings.GetList<string>(new string[] { "SystemFrame", "Cors", "Methods" }).ToArray();
                    if (methods.Any())
                    {
                        policy.WithMethods(methods);
                    }
                    else
                    {
                        policy.AllowAnyMethod();
                    }
                    // 允许任意请求头
                    policy.AllowAnyHeader();
                    // 允许客户端读取服务端响应头Header中哪些信息
                    policy.WithExposedHeaders(Appsettings.GetList<string>(new string[] { "SystemFrame", "Cors", "ExposedHeaders" }).ToArray());
                });
            });
        }

        public static void UseCorsSetup(this IApplicationBuilder app)
        {
            app.UseCors("Crx.vNext.Cors");
        }
    }
}
