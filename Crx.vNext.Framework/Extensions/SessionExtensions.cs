using Crx.vNext.Common.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
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
            if(Appsettings.GetBool(new[] { "SystemFrame", "EnabledSession" }) ?? false)
            {
                if (Appsettings.GetBool(new[] { "Redis", "Enabled" }) ?? false)
                {
                    services.AddStackExchangeRedisCache(o =>
                    {
                        o.Configuration = Appsettings.GetString(new[] { "Redis", "Connection" });
                        o.ConfigurationOptions = new ConfigurationOptions
                        {
                            ClientName = Appsettings.GetString(new[] { "Redis", "ClientName" }) + "Session:"
                        };
                    });
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
