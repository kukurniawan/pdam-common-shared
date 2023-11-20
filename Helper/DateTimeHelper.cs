using System;

namespace Pdam.Common.Shared.Helper;

/// <summary>
/// 
/// </summary>
public static class DateTimeHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int ToPeriod(this DateTime value)
    {
        var s = $"{value.Year}{value.Month:D2}";
        return Convert.ToInt32(s);
    }
    
    public static int ToPeriodYear(this DateTime value)
    {
        var s = $"{value.Year}00";
        return Convert.ToInt32(s);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTime ToSEAsiaStandardTime(this DateTime value)
    {
        var specifyKind = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var gmt7Time = TimeZoneInfo.ConvertTime(specifyKind, timeZoneInfo);
        return gmt7Time;
    }
}