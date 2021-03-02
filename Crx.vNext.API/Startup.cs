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
            /*** 优先 ***/
            _ = new Appsettings(Configuration);
            _ = new SnowflakeID(new(Appsettings.GetLong("SystemFrame:Snowflake:WorkerId").Value,
                Appsettings.GetLong("SystemFrame:Snowflake:DatacenterId").Value,
                Appsettings.GetLong("SystemFrame:Snowflake:Sequence").Value));
            SerilogExtensions.AddSerilogSetup(Configuration);

            /*** 注入各类服务 ***/
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

        // Autofac容器
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnection>().As<IDbConnection>()
                .WithParameter("connectionString", Configuration.GetConnectionString("WriteConnection"));

            var aopType = builder.GetAopList();

            //注册要通过反射创建的组件
            var assemblyService = Assembly.LoadFrom(Path.Combine(AppContext.BaseDirectory, "Crx.vNext.Service.dll"));
            builder.RegisterAssemblyTypes(assemblyService)
                .AsImplementedInterfaces() // 接口注入
                .InstancePerDependency() // 每次都是新实例
                .EnableInterfaceInterceptors().InterceptedBy(aopType); 
            var assemblyRepository = Assembly.LoadFrom(Path.Combine(AppContext.BaseDirectory, "Crx.vNext.Repository.dll"));
            builder.RegisterAssemblyTypes(assemblyRepository)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .EnableInterfaceInterceptors().InterceptedBy(aopType);
            //builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();//泛型注入            

            // 工作单元注入为Scope，放在反射注入之后
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope(); // 同一次请求是相同实例
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
            // 路由
            app.UseRouting();
            // 先开启认证
            //app.UseAuthentication();
            // 然后是授权中间件
            //app.UseAuthorization();

            app.UseSerilogSetup();

            app.UseSessionSetup();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // 这些不是中间件
            app.ConsulRegister();
        }
    }
}
