using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Pdam.Common.Shared.Security;

/// <summary>
/// 
/// </summary>
public class AuthenticationInfo
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Roles { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Guid Uid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string UserType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jwtToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static AuthenticationInfo Validate((ClaimsPrincipal principal, JwtSecurityToken) jwtToken)
    {
        return new AuthenticationInfo
        {
            Email = jwtToken.principal.Claims.FirstOrDefault(x=>x.Type == JwtAuthManager.CLAIM_TYPE_EMAIL)?.Value ?? throw new InvalidOperationException(),
            Name = jwtToken.principal.Claims.FirstOrDefault(x=>x.Type == JwtAuthManager.CLAIM_TYPE_NAME)?.Value ?? throw new InvalidOperationException(),
            Roles = jwtToken.principal.Claims.FirstOrDefault(x=>x.Type == JwtAuthManager.CLAIM_TYPE_ROLE)?.Value ?? throw new InvalidOperationException(),
            Uid = new Guid(jwtToken.principal.Claims.FirstOrDefault(x=>x.Type == JwtAuthManager.CLAIM_TYPE_UID)?.Value ?? throw new InvalidOperationException()),
            UserType = jwtToken.principal.Claims.FirstOrDefault(x=>x.Type == JwtAuthManager.CLAIM_USER_TYPE)?.Value ?? throw new InvalidOperationException()
        };
    }
}