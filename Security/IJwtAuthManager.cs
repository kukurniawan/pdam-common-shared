using System;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Pdam.Common.Shared.Security;

public interface IJwtAuthManager
{
    IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
    JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);
    JwtAuthResult Refresh(string refreshToken, string accessToken, string email, DateTime now);
    void RemoveExpiredRefreshTokens(DateTime now);
    void RemoveRefreshTokenByUserName(string userName);
    (ClaimsPrincipal principal, JwtSecurityToken) DecodeJwtToken(string token);
    string GetClaim(string token, string key);
    string GetClaim(string key);
    Task<Guid> GetValidUserClaimUid();
}