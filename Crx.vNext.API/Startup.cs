using Autofac;
using Crx.vNext.Common.Base;
using Crx.vNext.Framework.Extensions;
using Crx.vNext.Framework.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crx.vNext.API
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
            #region Serilog
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
            #endregion
            
            services.AddSingleton(new Appsettings(Configuration));

            // �Ƿ���ͬ��
            if (Appsettings.GetBool(new[] { "SystemFrame", "AllowSync" }) ?? false)
            {
                services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                        .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);
            }
            services.AddSwaggerSetup();
            services.AddControllerSetup();
        } 
        
        // Autofac����
        public void ConfigureContainer(ContainerBuilder builder)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerSetup();

            //app.UseHttpsRedirection();

            app.UseStaticFiles();
            // ·��
            app.UseRouting();
            // �ȿ�����֤
            ///app.UseAuthentication();
            // Ȼ������Ȩ�м��
            //app.UseAuthorization();

            app.UseSerilogRequestLogging(); // ��Ҫ��ϸ��־ʱ���� Microsoft ����Ϊ Information

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
