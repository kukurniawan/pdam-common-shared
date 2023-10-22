using System;

namespace Pdam.Common.Shared.Security;
/// <summary>
/// 
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// 
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Role { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string OriginalUserName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string AccessToken { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string RefreshToken { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string CompanyId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool? ForceChangePassword { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool? ForceReLogin { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? PhotoUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Guid Uid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime ExpireAt { get; set; }
}