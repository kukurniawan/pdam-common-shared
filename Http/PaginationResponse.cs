using System.Collections.Generic;

namespace Pdam.Common.Shared.Http;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginationResponse<T> : PaginationRequest
{
    private readonly IEnumerable<T> _items;
    private readonly int _page;
    private readonly int _pageSize;
    private readonly int _totalCount;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalCount"></param>
    /// <param name="sortBy"></param>
    public PaginationResponse(IEnumerable<T> items, int page, int pageSize, int totalCount, string sortBy)
    {
        _items = items;
        _page = page;
        _pageSize = pageSize;
        _totalCount = totalCount;
        SortBy = sortBy;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalCount"></param>
    /// <param name="sortBy"></param>
    public PaginationResponse(IEnumerable<T> items, int page, int pageSize, int totalCount)
    {
        _items = items;
        _page = page;
        _pageSize = pageSize;
        _totalCount = totalCount;
    }
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<T> Items => _items;

    /// <summary>
    /// 
    /// </summary>
    public new int Page => _page;

    /// <summary>
    /// 
    /// </summary>
    public new int PageSize => _pageSize;

    /// <summary>
    /// 
    /// </summary>
    public int TotalCount => _totalCount;
}