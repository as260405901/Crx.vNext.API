using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Crx.vNext.Framework.Extensions
{
    /// <summary>
    /// Swagger接口文档配置
    /// </summary>
    public static class SwaggerExtensions
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            // 不需要显示的接口加特性标签 [ApiExplorerSettings(IgnoreApi = true)]
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Crx.vNext.API——接口文档",
                    Description = "Crx.vNext.API V1",
                });
                //c.OrderActionsBy(o => o.RelativePath); // 排序规则？？？

                var basePath = AppContext.BaseDirectory; // 程序所在bin文件夹
                // 接口描述，不想每个都加描述可以取消警告，代码：1591
                var xmlPath = Path.Combine(basePath, "Crx.vNext.API.xml"); //Xml描述文件所在位置
                c.IncludeXmlComments(xmlPath, true);//参数2：启用Controller类的描述
                // Model层描述
                var xmlModelPath = Path.Combine(basePath, "Crx.vNext.Model.xml");
                c.IncludeXmlComments(xmlModelPath);//Model的类描述会自动显示


                #region Token绑定到ConfigureServices
                /*
                // 在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                // Jwt Bearer 认证，必须是 oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });*/
                #endregion
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crx.vNext.API V1");
                c.RoutePrefix = "Swagger";// 接口文档访问路径，为空时表示根路径
            });
        }
    }
}
