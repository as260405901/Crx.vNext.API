using Consul;
using Crx.vNext.Common.Helper;
using Microsoft.AspNetCore.Builder;
using System;

namespace Crx.vNext.Framework.Extensions
{
    public static class ConsulExtensions
    {
        public static void ConsulRegister(this IApplicationBuilder app)
        {
            if (Appsettings.GetBool(new[] { "Consul", "Enabled" }) ?? false)
            {
                var client = new ConsulClient(c =>
                {
                    c.Address = new Uri(Appsettings.GetString(new[] { "Consul", "ConsulAddress" }));
                    c.Datacenter = Appsettings.GetString(new[] { "Consul", "Datacenter" });
                });
                var register = Appsettings.GetModel<AgentServiceRegistration>(new[] { "Consul", "Registration" });                
                client.Agent.ServiceRegister(register);
            }
        }
    }
}
