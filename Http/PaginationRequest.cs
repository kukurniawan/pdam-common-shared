using System.ComponentModel;

namespace Pdam.Common.Shared.Http;

/// <summary>
/// 
/// </summary>
public class PaginationRequest
{
    /// <summary>
    /// 
    /// </summary>
    [DefaultValue(1)]
    public int Page { get; set; } = 1;
    /// <summary>
    /// 
    /// </summary>
    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;
    /// <summary>
    /// 
    /// </summary>
    [DefaultValue("CreatedDate asc")]
    public string SortBy { get; set; } = "CreatedDate asc";
}