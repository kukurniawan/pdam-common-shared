using Microsoft.Extensions.Configuration;

namespace Pdam.Common.Shared.Security;

public class ApplicationSettings
{
    public ApplicationSettings(IConfiguration configuration)
    {
        JwtCookieName = configuration.GetSection("JwtCookieName").Value ?? throw new InvalidOperationException();
        RefreshTokenCookieName = configuration.GetSection("RefreshTokenCookieName").Value ?? throw new InvalidOperationException();
        RefreshTokenEncryptionPassPhrase = configuration.GetSection("RefreshTokenEncryptionPassPhrase").Value ?? throw new InvalidOperationException();
        CookieExpirationHours = Convert.ToDouble(configuration.GetSection("CookieExpirationHours").Value);
        SessionRefreshHours = Convert.ToDouble(configuration.GetSection("SessionRefreshHours").Value);
        ApplicationKey = configuration.GetSection("ApplicationKey").Value ?? throw new InvalidOperationException();
        PassPhase = configuration.GetSection("PassPhaseKey").Value ?? throw new InvalidOperationException();
        CompanyIdKey = configuration.GetSection("CompanyIdKey").Value ?? throw new InvalidOperationException();
    }
    public string JwtCookieName { get; set; }
    public string RefreshTokenCookieName { get; set; }
    public string RefreshTokenEncryptionPassPhrase { get; set; }
    public double CookieExpirationHours { get; set; }
    public double SessionRefreshHours { get; set; }
    public string ApplicationKey { get; set; }
    public string PassPhase { get; set; }
    public string CompanyIdKey { get; set; }
}