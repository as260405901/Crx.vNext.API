using Autofac;
using Crx.vNext.Common.Base;
using Crx.vNext.Framework.Extensions;
using Crx.vNext.Framework.Filter;
using Crx.vNext.Model;
using Crx.vNext.Model.Enum;
using Crx.vNext.Model.InputModel;
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
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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
            /*** 优先 ***/
            _ = new Appsettings(Configuration);
            Configuration.AddSerilogSetup();

            /*** 注入各类服务 ***/
            services.AddServerOptionsSetup();
            services.AddAutoMapperSetup();
            services.AddSwaggerSetup();
            services.AddControllerSetup();
        } 
        
        // Autofac容器
        public void ConfigureContainer(ContainerBuilder builder)
        {
            /*
            builder.RegisterType<SqlConnection>().As<IDbConnection>()
                .WithParameter("connectionString", Configuration.GetConnectionString("DefaultConnection"));

            //注册要通过反射创建的组件
            var assemblyService = Assembly.LoadFrom(Path.Combine(AppContext.BaseDirectory, "NCR.Service.dll"));
            builder.RegisterAssemblyTypes(assemblyService)
                .AsImplementedInterfaces() // 接口注入
                .InstancePerDependency();
            var assemblyRepository = Assembly.LoadFrom(Path.Combine(AppContext.BaseDirectory, "NCR.Repository.dll"));
            builder.RegisterAssemblyTypes(assemblyRepository)
                .AsImplementedInterfaces()
                .InstancePerDependency();
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();//泛型注入

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope(); // 工作单元注入为Scope
            */
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

            app.UseSerilogSetup();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
