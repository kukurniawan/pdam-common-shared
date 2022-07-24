using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Pdam.Common.Shared.Fault;
using Pdam.Common.Shared.Http;

namespace Pdam.Common.Shared.Security;

public class JwtAuthManager : IJwtAuthManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary => _usersRefreshTokens.ToImmutableDictionary();
    private readonly ConcurrentDictionary<string, RefreshToken> _usersRefreshTokens;  // can store in a database or a distributed cache
    private readonly byte[] _secret;
    internal const string CLAIM_TYPE_EMAIL = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    internal const string CLAIM_TYPE_NAME = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
    internal const string CLAIM_TYPE_ROLE = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    internal const string CLAIM_TYPE_UID = "uid";
    public JwtAuthManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _usersRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
        _secret = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtKey") ?? throw new InvalidOperationException("Invalid JWT Key"));
    }

    // optional: clean up expired refresh tokens
    public void RemoveExpiredRefreshTokens(DateTime now)
    {
        var expiredTokens = _usersRefreshTokens.Where(x => x.Value.ExpireAt < now).ToList();
        foreach (var expiredToken in expiredTokens)
        {
            _usersRefreshTokens.TryRemove(expiredToken.Key, out _);
        }
    }

    // can be more specific to ip, user agent, device name, etc.
    public void RemoveRefreshTokenByUserName(string userName)
    {
        var refreshTokens = _usersRefreshTokens.Where(x => x.Value.UserName == userName).ToList();
        foreach (var refreshToken in refreshTokens)
        {
            _usersRefreshTokens.TryRemove(refreshToken.Key, out _);
        }
    }

    public JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now)
    {
        var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
        var jwtToken = new JwtSecurityToken(
            Environment.GetEnvironmentVariable("JwtIssuer"),
            shouldAddAudienceClaim ? Environment.GetEnvironmentVariable("JwtAudience") : string.Empty,
            claims,
            expires: now.AddHours(Convert.ToInt32(Environment.GetEnvironmentVariable("JwtTokenDuration"))),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        var refreshToken = new RefreshToken
        {
            UserName = username,
            TokenString = GenerateRefreshTokenString(),
            ExpireAt = now.AddHours(Convert.ToInt32(Environment.GetEnvironmentVariable("JwtRefreshTokenDuration")))
        };
        _usersRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (_, _) => refreshToken);

        return new JwtAuthResult
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public JwtAuthResult Refresh(string refreshToken, string accessToken, string email, DateTime now)
    {
        var (principal, jwtToken) = DecodeJwtToken(accessToken);
        if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
        {
            throw new SecurityTokenException("Invalid token");
        }

        if (!_usersRefreshTokens.TryGetValue(refreshToken, out _))
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        return GenerateTokens(email, principal.Claims.ToArray(), now); // need to recover the original claims
    }

    public (ClaimsPrincipal principal, JwtSecurityToken) DecodeJwtToken(string token)
    {
        /*if (_httpContextAccessor.HttpContext?.Request.Path == "/account/refresh-token")
        {
            
        }*/
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new SecurityTokenException("Invalid token");
        }
        var principal = new JwtSecurityTokenHandler()
            .ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Environment.GetEnvironmentVariable("JwtIssuer"),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secret),
                    ValidAudience = Environment.GetEnvironmentVariable("JwtAudience"),
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                },
                out var validatedToken);
        return (principal, validatedToken as JwtSecurityToken);
    }

    public string GetClaim(string token, string key)
    {
        var cl = DecodeJwtToken(token);
        var claim = cl.principal.Claims.FirstOrDefault(x => x.Type == key);
        if (claim == null) throw new ApiException(ErrorDetail.NoClaim);
        return claim.Value;
    }
    
    public async Task<Guid> GetValidUserClaimUid()
    {
        var uid = GetClaim(CLAIM_TYPE_UID);
        /*var user = await _context.Users.FirstOrDefaultAsync(x => x.UID == new Guid(uid));
        if (user == null) throw new CoreServiceException(ErrorMessage.ACCOUNT_INVALID_ACCESS);*/
        return new Guid(uid);
    }
    
    public string GetClaim(string key)
    {
        var token = _httpContextAccessor.GetToken();
        var cl = DecodeJwtToken(token);
        var claim = cl.principal.Claims.FirstOrDefault(x => x.Type == key);
        if (claim == null)
            throw new SecurityException("Invalid or token has been expire");
        return claim.Value;
    }

    private static string GenerateRefreshTokenString()
    {
        var randomNumber = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}