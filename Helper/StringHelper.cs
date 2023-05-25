// ReSharper disable CheckNamespace

using System;

namespace Pdam.Common.Shared.Helper
    // ReSharper restore CheckNamespace
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveTrailingSlash(this string value) => value.EndsWith("/") ? value.TrimEnd('/') : value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string value)
        {
            return value == "1" || value.ToLower() == "true";
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInteger(this string value, int defaultValue = 0)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return defaultValue;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? ToNullableInteger(this string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) return null;
                return Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string DefaultValue(this string value, string defaultValue = "")
        {
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid? ToGuid(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            return new Guid(value);
        }
    }
}