using System;
using Microsoft.Extensions.Configuration;

namespace Pdam.Common.Shared.Security;

/// <summary>
/// 
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <exception cref="InvalidOperationException"></exception>
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
        StorageUrl =  configuration.GetSection("StorageUrl").Value ?? string.Empty;
        StorageAccessToken =  configuration.GetSection("StorageAccessToken").Value ?? string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    public string StorageAccessToken { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string StorageUrl { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string JwtCookieName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string RefreshTokenCookieName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string RefreshTokenEncryptionPassPhrase { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double CookieExpirationHours { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double SessionRefreshHours { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string ApplicationKey { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string PassPhase { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string CompanyIdKey { get; set; }
}