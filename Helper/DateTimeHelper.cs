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
}