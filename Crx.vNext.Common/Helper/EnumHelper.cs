using System;
using Microsoft.OpenApi.Extensions;
using System.ComponentModel;

namespace Crx.vNext.Common.Helper
{
    /// <summary>
    /// 枚举类型帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取特性标签的描述内容
        /// </summary>
        public static string GetDescription(this Enum value)
        {
            return value.GetAttributeOfType<DescriptionAttribute>().Description;
        }
    }
}
