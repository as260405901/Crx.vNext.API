using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crx.vNext.Common.Base
{
    /// <summary>
    /// 配置文件操作类
    /// </summary>
    public class Appsettings
    {
        private static IConfiguration Configuration { get; set; }

        public Appsettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 字符串配置
        /// </summary>
        public static string GetString(params string[] sections)
        {
            if (sections.Any())
            {
                return Configuration[string.Join(":", sections)];
            }
            return null;
        }

        /// <summary>
        /// 字符串配置
        /// </summary>
        public static bool? GetBool(params string[] sections)
        {
            var str = GetString(sections);
            if (str != null && bool.TryParse(str, out bool result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 配置转实体
        /// </summary>
        public static T GetModel<T>(params string[] sections) where T : new()
        {
            T model = new T();
            Configuration.Bind(string.Join(":", sections), model);
            return model;
        }

        /// <summary>
        /// 配置转实体集合
        /// </summary>
        public static List<T> GetList<T>(params string[] sections)
        {
            List<T> list = new List<T>();
            Configuration.Bind(string.Join(":", sections), list);
            return list;
        }
    }
}
