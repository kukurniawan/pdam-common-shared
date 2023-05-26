using System;
using System.Linq;

namespace Pdam.Common.Shared.Data;

/// <summary>
/// 
/// </summary>
public class SortingHelper
{
    /// <summary>
    /// if sort in external column (base on query table) then T2 = true 
    /// </summary>
    /// <param name="type">type of default entity</param>
    /// <param name="value">value of sort by with ":" separator</param>
    /// <param name="paramList">list exclude columns</param>
    /// <returns></returns>
    public static string GetDynamicSortBy(Type type, string value, SortParamList paramList)
    {
        var sortParams = value.Split(':');
        var columnName = sortParams[0];
        var sortOrder = sortParams.Length > 1 ? sortParams[1] : "asc";
        if (paramList.Any(x => string.Equals(x.ColumnName, columnName, StringComparison.CurrentCultureIgnoreCase)))
        {
            var param = paramList.FirstOrDefault(x => string.Equals(x.ColumnName, columnName, StringComparison.CurrentCultureIgnoreCase));
            if (param != null) return $"{param.SortColumn} {sortOrder}";
        }
        var properties = type.GetProperties();
        var matchProperty = properties.FirstOrDefault(x => x.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
        return matchProperty == null ? $"{properties[0].Name} ASC" : $"{matchProperty.Name} {sortOrder}";
    }
}