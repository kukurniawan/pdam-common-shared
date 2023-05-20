using System.Net;
using System.Text;
using Pdam.Common.Shared.Fault;
using Radzen;

namespace Pdam.Common.Shared.Data;

/// <summary>
/// lambda helper
/// </summary>
public static class LambdaHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="except"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Tuple<string, object[]> GetDynamicWhereTerm<T>(T obj, params string[] except) where T : class
    {
        var s = new StringBuilder();
        var f = new List<object>();
        var g = obj.GetType().GetProperties().Where(x =>
            !string.Equals(x.Name, "SortBy", StringComparison.CurrentCultureIgnoreCase) &&
            !string.Equals(x.Name, "Page", StringComparison.CurrentCultureIgnoreCase) &&
            !string.Equals(x.Name, "Skip", StringComparison.CurrentCultureIgnoreCase) &&
            !string.Equals(x.Name, "Top", StringComparison.CurrentCultureIgnoreCase) &&
            !string.Equals(x.Name, "PageSize", StringComparison.CurrentCultureIgnoreCase));
        var counter = 0;
        foreach (var property in g.Where(x=> !except.Contains(x.Name)))
        {
            var name = property.Name;
            var value = property.GetValue(obj);
                
            if (value == null) continue;
            var dataType = property.PropertyType.Name.ToLower();
            //if (dataType == "datetime") continue;
            if (counter != 0)
                s.Append(" AND ");
            if (dataType == "string")
                s.Append($"{name}.Contains(@{counter})");
            if (dataType == "datetime")
            {
                if (name.ToLower() == "modifieddate")
                    s.Append($"{name} <= @{counter}");
                else
                    s.Append($"{name} >= @{counter}");
            }
            else
                s.Append($"{name} = @{counter}");
            f.Add(value);
            counter++;
        }

        return new Tuple<string, object[]>(s.ToString(), f.ToArray());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="mapper"></param>
    /// <param name="filters"></param>
    /// <param name="logicalFilterOperator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Tuple<string, object[]> GetDynamicWhereTerm<T>(T obj, SortParamList mapper, IEnumerable<FilterDescriptor>? filters, LogicalFilterOperator logicalFilterOperator) where T : class
    {
        var s = new StringBuilder();
        var f = new List<object>();
        var g = obj.GetType().GetProperties().Where(x =>
            !string.Equals(x.Name, "SortBy", StringComparison.CurrentCultureIgnoreCase) &&
            !string.Equals(x.Name, "Page", StringComparison.CurrentCultureIgnoreCase) &&
            !string.Equals(x.Name, "Skip", StringComparison.CurrentCultureIgnoreCase) &&
            !string.Equals(x.Name, "Top", StringComparison.CurrentCultureIgnoreCase) &&
            !string.Equals(x.Name, "Filters", StringComparison.CurrentCultureIgnoreCase) && 
            !string.Equals(x.Name, "LogicalFilterOperatorByField", StringComparison.CurrentCultureIgnoreCase) &&
            !string.Equals(x.Name, "PageSize", StringComparison.CurrentCultureIgnoreCase));
        var counter = 0;
        foreach (var property in g.Where(x=> !mapper.Select(c=>c.ColumnName).Contains(x.Name)))
        {
            var name = property.Name;
            var value = property.GetValue(obj);
                
            if (value == null) continue;
            var dataType = property.PropertyType.Name.ToLower();
            //if (dataType == "datetime") continue;
            if (counter != 0)
                s.Append(" OR ");
            if (dataType == "string")
                s.Append($"{name}.Contains(@{counter})");
            if (dataType == "datetime")
            {
                if (name.ToLower() == "modifieddate")
                    s.Append($"{name} <= @{counter}");
                else
                    s.Append($"{name} >= @{counter}");
            }
            else
                s.Append($"{name} = @{counter}");
            f.Add(value);
            counter++;
        }

        if (filters == null) return new Tuple<string, object[]>(s.ToString(), f.ToArray());
        var filterDescriptors = filters.ToList();
        if (filterDescriptors.Any() && s.Length > 0) 
            s.Append(" OR ");

        var index = 0;
        foreach (var filter in filterDescriptors)
        {
            if (mapper.Any(x => x.ColumnName == filter.Property))
            {
                if (s.Length > 0 /*&& (index + 1) < filterDescriptors.Count*/) s.Append( logicalFilterOperator == LogicalFilterOperator.And ? " AND " : " OR ");
                var column = mapper.FirstOrDefault(x => x.ColumnName == filter.Property);
                if (column == null) continue;
                var param1 = GenerateLinqDynamic(column, filter.FilterOperator, counter);
                var ops = "";
                StringBuilder? param2 = null;
                if (filter.FilterOperator is not (FilterOperator.IsNull or FilterOperator.IsNotNull))
                {
                    f.Add(filter.FilterOperator is FilterOperator.IsEmpty or FilterOperator.IsNotEmpty
                        ? ""
                        : filter.FilterValue);

                    counter++;
                }

                if (filter.SecondFilterOperator == FilterOperator.IsNull ||
                    filter.SecondFilterOperator == FilterOperator.IsNotNull)
                {
                    ops = filter.LogicalFilterOperator == LogicalFilterOperator.Or ? "OR " : "AND ";
                    column = mapper.FirstOrDefault(x => x.ColumnName == filter.Property);
                    if (column == null) continue;
                    param2 = GenerateLinqDynamic(column, filter.FilterOperator, counter);
                }
                else
                {
                    if (filter.SecondFilterValue != null && !string.IsNullOrEmpty(filter.SecondFilterValue.ToString()) && (filter.SecondFilterOperator != FilterOperator.IsEmpty ||
                                                             filter.SecondFilterOperator != FilterOperator.IsNotEmpty))
                    {
                        ops = filter.LogicalFilterOperator == LogicalFilterOperator.Or ? "OR " : "AND ";
                        column = mapper.FirstOrDefault(x => x.ColumnName == filter.Property);
                        if (column == null) continue;
                        param2 = GenerateLinqDynamic(column, filter.FilterOperator, counter);
                        f.Add(filter.SecondFilterValue);
                        counter++;
                    }
                    else if (filter.SecondFilterOperator == FilterOperator.IsEmpty ||
                             filter.SecondFilterOperator == FilterOperator.IsNotEmpty)
                    {
                        ops = filter.LogicalFilterOperator == LogicalFilterOperator.Or ? "OR " : "AND ";
                        column = mapper.FirstOrDefault(x => x.ColumnName == filter.Property);
                        if (column == null) continue;
                        param2 = GenerateLinqDynamic(column, filter.FilterOperator, counter);
                        f.Add("");
                        counter++;
                    }
                }

                if (param2 == null)
                    s.Append(param1);
                else
                    s.Append($"({param1} {ops} {param2})");
            }
            else
            {
                throw new ApiException(new ErrorDetail($"Invalid column name {filter.Property}", "2400",
                    HttpStatusCode.BadRequest));
            }
            index++;
        }

        return new Tuple<string, object[]>(s.ToString(), f.ToArray());
    }

    private static StringBuilder GenerateLinqDynamic(SortParam sortParam, FilterOperator filterOperator, int counter)
    {
        var a = new StringBuilder();
        switch (filterOperator)
        {
            case FilterOperator.Equals:
                a.Append($"{sortParam.SortColumn} == @{counter} ");
                break;
            case FilterOperator.NotEquals:
                a.Append($"{sortParam.SortColumn} != @{counter} ");
                break;
            case FilterOperator.LessThan:
                a.Append($"{sortParam.SortColumn} < @{counter} ");
                break;
            case FilterOperator.LessThanOrEquals:
                a.Append($"{sortParam.SortColumn} <= @{counter} ");
                break;
            case FilterOperator.GreaterThan:
                a.Append($"{sortParam.SortColumn} > @{counter} ");
                break;
            case FilterOperator.GreaterThanOrEquals:
                a.Append($"{sortParam.SortColumn} >= @{counter} ");
                break;
            case FilterOperator.Contains:
                a.Append($"{sortParam.SortColumn}.Contains(@{counter}) ");
                break;
            case FilterOperator.StartsWith:
                a.Append($"{sortParam.SortColumn}.StartsWith(@{counter}) ");
                break;
            case FilterOperator.EndsWith:
                a.Append($"{sortParam.SortColumn}.EndsWith(@{counter}) ");
                break;
            case FilterOperator.DoesNotContain:
                a.Append($"!{sortParam.SortColumn}.Contains(@{counter}) ");
                break;
            case FilterOperator.IsNull:
                a.Append($"{sortParam.SortColumn} == null ");
                break;
            case FilterOperator.IsEmpty:
                a.Append($"{sortParam.SortColumn} == @{counter} ");
                break;
            case FilterOperator.IsNotNull:
                a.Append($"{sortParam.SortColumn} != null ");
                break;
            case FilterOperator.IsNotEmpty:
                a.Append($"{sortParam.SortColumn} != @{counter} ");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return a;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetDynamicSortBy(Type type, string value)
    {
        var sortParams = value.Split(';');
        var f = SortParamList.Instance;
        foreach (var param in sortParams)
        {
            var g = param.Split(" ");
            switch (g.Length)
            {
                case > 1:
                    f.Add(new SortParam
                    {
                        ColumnName = g[0],
                        SortColumn = g[0],
                        SortOrder = g[1]
                    });
                    break;
                case 1:
                    f.Add(new SortParam
                    {
                        ColumnName = g[0],
                        SortColumn = g[0]
                    });
                    break;
                default:
                    throw new ApiException(new ErrorDetail($"Invalid column name {param}", "2400", HttpStatusCode.BadRequest));
            }
        }
        var properties = type.GetProperties();
        var matchProperty = properties.FirstOrDefault(x => f.Select(c=>c.SortColumn.ToLower()).Contains(x.Name.ToLower()));
        if (matchProperty == null)
            throw new ApiException(new ErrorDetail($"Invalid column name {value}", "2400", HttpStatusCode.BadRequest));
        return string.Join(",", f.Select(x => $"{x.SortColumn} {x.SortOrder}".Trim()));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="sortParamList"></param>
    /// <returns></returns>
    /// <exception cref="ApiException"></exception>
    public static string GetDynamicSortBy(Type type, string value, SortParamList sortParamList)
    {
        var sortParams = value.Split(';');
        var f = SortParamList.Instance;
        foreach (var param in sortParams)
        {
            var g = param.Split(" ");
            switch (g.Length)
            {
                case > 1:
                    var l = sortParamList.FirstOrDefault(x => x.ColumnName == g[0]);
                    f.Add(new SortParam
                    {
                        ColumnName = g[0],
                        SortColumn = l == null ? g[0] : l.SortColumn,
                        SortOrder = CleanDirection(g[1]),
                        Outer = l != null
                    });
                    break;
                case 1:
                    var j = sortParamList.FirstOrDefault(x => x.ColumnName == g[0]);
                    f.Add(new SortParam
                    {
                        ColumnName = g[0],
                        SortColumn = j == null ? g[0] : j.SortColumn,
                        Outer = j != null
                    });
                    break;
                default:
                    throw new ApiException(new ErrorDetail($"Invalid column name {param}", "2400", HttpStatusCode.BadRequest));
            }
        }
        return string.Join(",", f.Select(x => $"{x.SortColumn} {x.SortOrder}".Trim()));
    }

    private static string CleanDirection(string direction)
    {
        if (string.IsNullOrEmpty(direction)) return "";
        return direction.ToLower() switch
        {
            "asc" => "ASC",
            "desc" => "DESC",
            _ => direction.ToLower() == "descending" ? "DESC" : "ASC"
        };
    }
}