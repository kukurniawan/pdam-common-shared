namespace Pdam.Common.Shared.Security;

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; }
    public Guid Uid { get; set; }
    public string CompanyId { get; set; }
}