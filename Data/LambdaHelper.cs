using System.Text;

namespace Pdam.Common.Shared.Data;

public class LambdaHelper
{
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
    
    public static string GetDynamicSortBy(Type type, string value)
    {
        var sortParams = value.Split(':');
        var columnName = sortParams[0];
        var sortOrder = sortParams.Length > 1 ? sortParams[1] : "asc";
        var properties = type.GetProperties();
        var matchProperty = properties.FirstOrDefault(x => x.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
        return matchProperty == null ? $"{properties[0].Name} ASC" : $"{matchProperty.Name} {sortOrder}";
    }
}