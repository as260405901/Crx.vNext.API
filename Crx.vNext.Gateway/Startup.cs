using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Administration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using System;

namespace Crx.vNext.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication("Bearer").AddJwtBearer("TestKey", o =>
            //{
            //    o.Authority = "http://localhost:30032";
            //    o.RequireHttpsMetadata = false;
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = false,
            //    };
            //});

            services.AddOcelot().AddConsul().AddPolly();//.AddAdministration("/administration", options); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseOcelot().Wait();
        }
    }
}
