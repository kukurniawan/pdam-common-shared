namespace Pdam.Common.Shared.Http;

/// <summary>
/// 
/// </summary>
public interface IHttpService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="resource"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    Task<HttpServiceResult<T>> PostAsJson<T, T2>(Uri uri, T2 resource, Action<HttpRequestMessage>? action = null)
        where T : class;
}