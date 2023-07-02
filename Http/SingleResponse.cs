namespace Pdam.Common.Shared.Http;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingleResponse<T>
{
    private readonly T _item;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public SingleResponse(T item)
    {
        _item = item;
    }
    /// <summary>
    /// 
    /// </summary>
    public T Item => _item;
}