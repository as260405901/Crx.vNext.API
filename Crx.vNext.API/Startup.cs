using Autofac;
using Autofac.Extras.DynamicProxy;
using Crx.vNext.Common.Helper;
using Crx.vNext.Framework.AOP;
using Crx.vNext.Framework.Extensions;
using Crx.vNext.IRepository.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

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
            _ = new SnowflakeID(new(Appsettings.GetLong("SystemFrame:Snowflake:WorkerId").Value,
                Appsettings.GetLong("SystemFrame:Snowflake:DatacenterId").Value,
                Appsettings.GetLong("SystemFrame:Snowflake:Sequence").Value));
            SerilogExtensions.AddSerilogSetup(Configuration);

            /*** ע�������� ***/
            //services.AddHttpContextAccessor();
            services.AddRedisSetup().Wait();
            services.AddServerOptionsSetup();
            services.AddAutoMapperSetup();
            services.AddSwaggerSetup();
            services.AddMiniProfilerSetup();
            services.AddCorsSetup();
            services.AddSessionSetup();
            services.AddControllerSetup();
            // Transient Scoped Singleton
        }

        // Autofac����
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnection>().As<IDbConnection>()
                .WithParameter("connectionString", Configuration.GetConnectionString("WriteConnection"));

            var aopType = builder.GetAopList();

            //ע��Ҫͨ�����䴴�������
            var assemblyService = Assembly.LoadFrom(Path.Combine(AppContext.BaseDirectory, "Crx.vNext.Service.dll"));
            builder.RegisterAssemblyTypes(assemblyService)
                .AsImplementedInterfaces() // �ӿ�ע��
                .InstancePerDependency() // ÿ�ζ�����ʵ��
                .EnableInterfaceInterceptors().InterceptedBy(aopType); 
            var assemblyRepository = Assembly.LoadFrom(Path.Combine(AppContext.BaseDirectory, "Crx.vNext.Repository.dll"));
            builder.RegisterAssemblyTypes(assemblyRepository)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .EnableInterfaceInterceptors().InterceptedBy(aopType);
            //builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();//����ע��            

            // ������Ԫע��ΪScope�����ڷ���ע��֮��
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope(); // ͬһ����������ͬʵ��
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseExceptionHandler("/Home/Error");
                // app.UseHttpsRedirection();
            }

            //app.UseStaticFiles();
            app.UseMiniProfilerSetup();
            app.UseSwaggerSetup();

            app.UseCorsSetup();
            // ·��
            app.UseRouting();
            // �ȿ�����֤
            //app.UseAuthentication();
            // Ȼ������Ȩ�м��
            //app.UseAuthorization();

            app.UseSerilogSetup();

            app.UseSessionSetup();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // ��Щ�����м��
            app.ConsulRegister();
        }
    }
}
