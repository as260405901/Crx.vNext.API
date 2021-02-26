using Crx.vNext.Common.Base;
using Crx.vNext.Common.Helper;
using Crx.vNext.Framework.Filter;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Crx.vNext.Framework.Extensions
{
    /// <summary>
    /// 控制器配置
    /// </summary>
    public static class ControllerExtensions
    {
        public static void AddControllerSetup(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                // 相同类型过滤器实现 IOrderedFilter 接口来调整执行顺序，从小到大执行
                // 全局请求参数校验过滤器
                options.Filters.Add(typeof(GlobalRequestParameterVerificationFilter));
                if (Appsettings.GetBool(new[] { "SystemFrame", "MandatoryResponseFormat" }) ?? false)
                {
                    // 全局请求返回值过滤器，强制使用统一请求响应格式
                    options.Filters.Add(typeof(GlobalResultFilter));
                }
                // 全局异常过滤器
                options.Filters.Add(typeof(GlobalExceptionsFilter));
            }).AddJsonOptions(o=> {
                // 全局设置编码不生效？？？
                // o.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                // o.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                // o.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.StringConverter());
                // 全局时间格式化
                o.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeConverter());
                o.JsonSerializerOptions.Converters.Add(new SystemTextJsonConvert.DateTimeNullableConverter());
            });
        }
    }
}
