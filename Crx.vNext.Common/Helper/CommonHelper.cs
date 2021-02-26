using Microsoft.OpenApi.Extensions;
using System;
using System.ComponentModel;

namespace Crx.vNext.Common.Helper
{
    public static class CommonHelper
    {
        /// <summary>
        /// 执行指定函数后释放对象
        /// </summary>
        /// <typeparam name="T">必须继承 IDisposable 接口</typeparam>
        /// <param name="func">释放前要执行的方法</param>
        public static void InvokeDispose<T>(this T obj, Action func = null) where T : IDisposable
        {
            if (obj != null)
            {
                func?.Invoke();
                obj.Dispose();
                obj = default;
            }
        }

        /// <summary>
        /// 获取特性标签的描述内容
        /// </summary>
        public static string GetDescription(this Enum value)
        {
            return value.GetAttributeOfType<DescriptionAttribute>().Description;
        }
    }
}
