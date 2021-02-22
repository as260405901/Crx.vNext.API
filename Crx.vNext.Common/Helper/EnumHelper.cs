using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

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
            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi == null)
            {
                return value.ToString();
            }
            object[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return ((DescriptionAttribute)attributes[0]).Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
