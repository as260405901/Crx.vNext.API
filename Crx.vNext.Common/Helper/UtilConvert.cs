using System;

namespace Crx.vNext.Common.Helper
{
    /// <summary>
    /// Object转普通类型
    /// </summary>
    public static class UtilConvert
    {
        public static readonly DateTime MinDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public static int ObjToInt(this object thisValue)
        {
            return thisValue.ObjToInt(0);
        }

        public static int ObjToInt(this object thisValue, int errorValue)
        {
            if (thisValue == null)
                return errorValue;
            else if (thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out int reval))
            {
                return reval;
            }
            return errorValue;
        }

        public static double ObjToMoney(this object thisValue)
        {
            return thisValue.ObjToMoney(0);
        }

        public static double ObjToMoney(this object thisValue, double errorValue)
        {
            if (thisValue == null)
                return errorValue;
            else if (thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out double reval))
            {
                return reval;
            }
            return errorValue;
        }

        public static string ObjToString(this object thisValue)
        {
            return thisValue.ObjToString(null);
        }

        public static string ObjToString(this object thisValue, string errorValue)
        {
            if (thisValue != null)
                return thisValue.ToString().Trim();
            return errorValue;
        }

        public static bool IsNotEmptyOrNull(this object thisValue)
        {
            var str = thisValue.ObjToString();
            return str != "" && str != "undefined" && str != "null";
        }

        public static decimal ObjToDecimal(this object thisValue)
        {
            return thisValue.ObjToDecimal(0);
        }

        public static decimal ObjToDecimal(this object thisValue, decimal errorValue)
        {
            if (thisValue == null)
                return errorValue;
            else if (thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out decimal reval))
            {
                return reval;
            }
            return errorValue;
        }

        public static DateTime ObjToDate(this object thisValue)
        {
            return thisValue.ObjToDate(MinDateTime);
        }

        public static DateTime ObjToDate(this object thisValue, DateTime errorValue)
        {
            if (thisValue == null)
                return errorValue;
            else if (thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out DateTime reval))
            {
                return reval;
            }
            return errorValue;
        }

        public static bool ObjToBool(this object thisValue)
        {
            return thisValue.ObjToBool(false);
        }

        public static bool ObjToBool(this object thisValue, bool errorValue)
        {
            if (thisValue == null)
                return errorValue;
            else if (thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out bool reval))
            {
                return reval;
            }
            return errorValue;
        }

        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        public static string DateToTimeStamp(this DateTime thisValue)
        {
            TimeSpan ts = thisValue - MinDateTime;
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
