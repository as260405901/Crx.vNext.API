using Autofac;
using Crx.vNext.Common.Base;
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
using NationalCompetitionRegistration.Extensions;
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

            // 是否开启同步
            if (Appsettings.GetBool("AllowSync") ?? false)
            {
                services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                        .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);
            }

            services.AddSwaggerSetup();
            services.AddControllers(options =>
            {
                // 相同类型过滤器实现 IOrderedFilter 接口来调整执行顺序，从小到大执行
                // 全局请求参数校验过滤器
                options.Filters.Add(typeof(GlobalRequestParameterVerificationFilter));
                // 全局异常过滤器
                options.Filters.Add(typeof(GlobalExceptionsFilter));
            });
        } 
        
        // Autofac容器
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
            // 路由
            app.UseRouting();
            // 先开启认证
            ///app.UseAuthentication();
            // 然后是授权中间件
            //app.UseAuthorization();

            app.UseSerilogRequestLogging(); // 需要详细日志时，将 Microsoft 设置为 Information

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
