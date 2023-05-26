using System;

namespace Pdam.Common.Shared.Security;

public class LoginResponse
{
    public string Email { get; set; }

    public string Role { get; set; }

    public string OriginalUserName { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
    public string CompanyId { get; set; }
    public bool? ForceChangePassword { get; set; }
    public bool? ForceReLogin { get; set; }
    public string? PhotoUrl { get; set; }
    public Guid Uid { get; set; }
}