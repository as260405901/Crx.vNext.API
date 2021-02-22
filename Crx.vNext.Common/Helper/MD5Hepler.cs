using System;
using System.Security.Cryptography;
using System.Text;

namespace Crx.vNext.Common.Helper
{
    public class MD5Helper
    {
        private static byte[] GetMd5ByteArray(string password)
        {
            MD5 md5 = MD5.Create();
            return md5.ComputeHash(Encoding.UTF8.GetBytes(password ?? string.Empty));
        }

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        public static string GetMD5_16bit(string password)
        {
            byte[] s = GetMd5ByteArray(password);
            return BitConverter.ToString(s, 4, 8).Replace("-", string.Empty);
        }


        /// <summary>
        /// 32位MD5加密
        /// </summary>
        public static string GetMD5(string password)
        {
            byte[] s = GetMd5ByteArray(password);
            return BitConverter.ToString(s).Replace("-", string.Empty);
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        public static string GetMD5_Salt(string password, string salt = "crx")
        {
            byte[] s = GetMd5ByteArray(password + salt);
            return BitConverter.ToString(s).Replace("-", string.Empty);
        }

        /// <summary>
        /// 64位MD5加密
        /// </summary>
        public static string GetMD5_64bit(string password)
        {
            byte[] s = GetMd5ByteArray(password);
            return Convert.ToBase64String(s);
        }

    }
}
