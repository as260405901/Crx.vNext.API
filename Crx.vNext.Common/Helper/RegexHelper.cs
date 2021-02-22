using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Crx.vNext.Common.Helper
{
    public static class RegexHelper
    {
        public static bool IsIdCard(this string str)
        {
            Regex regex = new Regex(@"^[1-9]\d{5}(18|19|20|(3\d))\d{2}((0[1-9])|(1[0-2]))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$");
            return regex.IsMatch(str);
        }


        public static bool IsPhone(this string str)
        {
            Regex regex = new Regex(@"^1[3456789]\d{9}$");
            return regex.IsMatch(str);
        }

        /// <summary>
        /// 座机
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLandline(this string str)
        {
            Regex regex = new Regex(@"^0\d{2,3}-\d{7,8}$");
            return regex.IsMatch(str);
        }

        /// <summary>
        /// 电话或座机
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPhoneOrLandline(this string str)
        {
            Regex regex = new Regex(@"^((0\d{2,3}-\d{7,8})|(1[3456789]\d{9}))$");
            return regex.IsMatch(str);
        }

        /// <summary>
        /// 获取base64图片的格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetBase64ImageExt(this string str)
        {
            var match = Regex.Match(str, @"^data:image/(\w+);base64,");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return null;
        }
    }
}
