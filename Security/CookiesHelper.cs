
using Microsoft.AspNetCore.Http;

namespace Pdam.Common.Shared.Security;
/// <summary>
/// 
/// </summary>
public class CookiesHelper
{
    private readonly ApplicationSettings _applicationSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookiesHelper(ApplicationSettings applicationSettings, IHttpContextAccessor httpContextAccessor)
    {
        _applicationSettings = applicationSettings;
        _httpContextAccessor = httpContextAccessor;
    }
    public static string CreateUrl(string jwtToken, string refreshToken, string applicationUid, string companyId)
    {
        return $"?jwtToken={Uri.EscapeDataString(jwtToken)}&refreshToken={Uri.EscapeDataString(refreshToken)}&" +
               $"redirectUrl={Uri.EscapeDataString("/")}&uid={Uri.EscapeDataString(applicationUid)}&companyId={Uri.EscapeDataString(companyId)}";
    }
    
    public static string CreateUrl(string jwtToken, string refreshToken, string applicationUid, string companyId, string redirectUrl)
    {
        return
            $"?jwtToken={Uri.EscapeDataString(jwtToken)}&refreshToken={Uri.EscapeDataString(refreshToken)}&uid={Uri.EscapeDataString(applicationUid)}&companyId={Uri.EscapeDataString(companyId)}&redirectUrl={Uri.EscapeDataString(redirectUrl)}";
    }
    
    public async Task<string> GetValue(string key)
    {
        try
        {
            var valueEncrypted = string.Empty;
            _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(key, out valueEncrypted);
            var decryptAsync = await EncryptionHelper.DecryptAsync(valueEncrypted ?? throw new InvalidOperationException(), _applicationSettings.PassPhase);
            return decryptAsync;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return string.Empty;
        }
    }
    
    public async Task SetValue(string key, string value)
    {
        var encryptAsync = await EncryptionHelper.EncryptAsync(value ?? throw new InvalidOperationException(), _applicationSettings.PassPhase);
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(
            key,
            encryptAsync,
            new CookieOptions
            {
                IsEssential = true,
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(_applicationSettings.CookieExpirationHours),
                SameSite = SameSiteMode.Strict
            });
    }
    
    /*public async Task<string> GetJwtToken()
    {
        var jwtTokenEncrypted = string.Empty;
        _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(_applicationSettings.JwtCookieName, out jwtTokenEncrypted);
        var decryptAsync = await EncryptionHelper.DecryptAsync(jwtTokenEncrypted ?? throw new InvalidOperationException(), _applicationSettings.PassPhase);
        return decryptAsync;
    }
    
    public async Task<string> GetRefreshToken()
    {
        var tokenEncrypted = string.Empty;
        _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(_applicationSettings.RefreshTokenCookieName, out tokenEncrypted);
        var decryptAsync = await EncryptionHelper.DecryptAsync(tokenEncrypted ?? throw new InvalidOperationException(), _applicationSettings.PassPhase);
        return decryptAsync;
    }
    
    public async Task<Guid> GetApplicationKey()
    {
        var tokenEncrypted = string.Empty;
        _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(_applicationSettings.ApplicationKey, out tokenEncrypted);
        var decryptAsync = await EncryptionHelper.DecryptAsync(tokenEncrypted ?? throw new InvalidOperationException(), _applicationSettings.PassPhase);
        return new Guid(decryptAsync);
    }*/
}