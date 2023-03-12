using System.Net;
using System.Text;
using Pdam.Common.Shared.Fault;

namespace Pdam.Common.Shared.Data;

/// <summary>
/// lambda helper
/// </summary>
public class LambdaHelper
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
                        SortOrder = g[1],
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
        var properties = type.GetProperties();
        var matchProperty = properties.FirstOrDefault(x => f.Where(v=> v.Outer == false)
            .Select(c=> c.SortColumn.ToLower()).Contains(x.Name.ToLower()));
        if (matchProperty == null)
            throw new ApiException(new ErrorDetail($"Invalid column name {value}", "2400", HttpStatusCode.BadRequest));
        return string.Join(",", f.Select(x => $"{x.SortColumn} {x.SortOrder}".Trim()));
    }
}