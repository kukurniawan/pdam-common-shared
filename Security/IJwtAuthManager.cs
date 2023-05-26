using System;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pdam.Common.Shared.Security;

/// <summary>
/// 
/// </summary>
public interface IJwtAuthManager
{
    /// <summary>
    /// 
    /// </summary>
    IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="claims"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <param name="accessToken"></param>
    /// <param name="email"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    JwtAuthResult Refresh(string refreshToken, string accessToken, string email, DateTime now);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="now"></param>
    void RemoveExpiredRefreshTokens(DateTime now);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    void RemoveRefreshTokenByUserName(string userName);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    (ClaimsPrincipal principal, JwtSecurityToken) DecodeJwtToken(string token);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    string GetClaim(string token, string key);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    string GetClaim(string key);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<Guid> GetValidUserClaimUid();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    AuthenticationInfo Authenticate();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jwtToken"></param>
    /// <returns></returns>
    AuthenticationInfo Authenticate(string jwtToken);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="roles"></param>
    /// <returns></returns>
    string IsAuthorized(params string[] roles);
}