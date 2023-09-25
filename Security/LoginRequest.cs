using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Pdam.Common.Shared.Security;

/// <summary>
/// 
/// </summary>
public class LoginRequest : IRequest<LoginResponse>
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string UserType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string Email { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string Password { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string CompanyId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string DeviceId { get; set; }
}