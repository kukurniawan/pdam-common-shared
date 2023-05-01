using System.ComponentModel.DataAnnotations;

namespace Pdam.Common.Shared.Security;

public class LoginRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string CompanyId { get; set; }
    [Required]
    public string DeviceId { get; set; }
}