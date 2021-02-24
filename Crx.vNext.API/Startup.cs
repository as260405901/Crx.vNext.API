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
            /*** ���� ***/
            _ = new Appsettings(Configuration);
            Configuration.AddSerilogSetup();

            /*** ע�������� ***/
            services.AddServerOptionsSetup();
            services.AddAutoMapperSetup();
            services.AddSwaggerSetup();
            services.AddControllerSetup();
        } 
        
        // Autofac����
        public void ConfigureContainer(ContainerBuilder builder)
        {
            /*
            builder.RegisterType<SqlConnection>().As<IDbConnection>()
                .WithParameter("connectionString", Configuration.GetConnectionString("DefaultConnection"));

            //ע��Ҫͨ�����䴴�������
            var assemblyService = Assembly.LoadFrom(Path.Combine(AppContext.BaseDirectory, "NCR.Service.dll"));
            builder.RegisterAssemblyTypes(assemblyService)
                .AsImplementedInterfaces() // �ӿ�ע��
                .InstancePerDependency();
            var assemblyRepository = Assembly.LoadFrom(Path.Combine(AppContext.BaseDirectory, "NCR.Repository.dll"));
            builder.RegisterAssemblyTypes(assemblyRepository)
                .AsImplementedInterfaces()
                .InstancePerDependency();
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();//����ע��

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope(); // ������Ԫע��ΪScope
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
            // ·��
            app.UseRouting();
            // �ȿ�����֤
            ///app.UseAuthentication();
            // Ȼ������Ȩ�м��
            //app.UseAuthorization();

            app.UseSerilogSetup();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
