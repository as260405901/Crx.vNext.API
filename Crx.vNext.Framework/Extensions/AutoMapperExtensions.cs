using Crx.vNext.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Crx.vNext.Framework.Extensions
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public static class AutoMapperExtensions
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfig));
        }
    }
}
