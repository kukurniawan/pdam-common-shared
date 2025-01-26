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
        JwtCookieName = configuration.GetSection("JwtCookieName").Value ?? string.Empty;
        RolesCookieName = configuration.GetSection("RolesCookieName").Value ?? string.Empty;
        RefreshTokenCookieName = configuration.GetSection("RefreshTokenCookieName").Value ?? string.Empty;
        RefreshTokenEncryptionPassPhrase = configuration.GetSection("RefreshTokenEncryptionPassPhrase").Value ?? string.Empty;
        CookieExpirationHours = Convert.ToDouble(configuration.GetSection("CookieExpirationHours").Value);
        SessionRefreshHours = Convert.ToDouble(configuration.GetSection("SessionRefreshHours").Value);
        ApplicationKey = configuration.GetSection("ApplicationKey").Value ?? string.Empty;
        PassPhase = configuration.GetSection("PassPhaseKey").Value ?? string.Empty;
        CompanyIdKey = configuration.GetSection("CompanyIdKey").Value ?? string.Empty;
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
    public string RolesCookieName { get; set; }

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