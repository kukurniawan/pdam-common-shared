namespace Pdam.Common.Shared.Http;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class ListResponse<T>
{
    private readonly IEnumerable<T> _items;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    public ListResponse(IEnumerable<T> items)
    {
        _items = items;
    }
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<T> Items => _items;
}