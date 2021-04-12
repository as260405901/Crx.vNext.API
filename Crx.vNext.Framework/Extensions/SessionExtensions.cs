using Crx.vNext.Common.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Crx.vNext.Framework.Extensions
{
    /// <summary>
    /// Session设置
    /// </summary>
    public static class SessionExtensions
    {
        public static void AddSessionSetup(this IServiceCollection services)
        {
            if(Appsettings.GetBool(new[] { "SystemFrame", "Session", "Enabled" }) ?? false)
            {
                if (Appsettings.GetBool(new[] { "SystemFrame", "Session", "Redis" }) ?? false)
                {
                    services.AddStackExchangeRedisCache(o => o.ConfigurationOptions = RedisHelper.Configuration);
                }
                else
                {
                    services.AddDistributedMemoryCache();
                }
                services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromSeconds(10);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });
            }            
        }

        public static void UseSessionSetup(this IApplicationBuilder app)
        {
            if (Appsettings.GetBool(new[] { "SystemFrame", "EnabledSession" }) ?? false)
            {
                app.UseSession();
            }
        }
    }
}
