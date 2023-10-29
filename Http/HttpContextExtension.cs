using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Pdam.Common.Shared.Fault;

namespace Pdam.Common.Shared.Http;

public static class HttpContextExtension
{

    /// <summary>
    /// get jwt token from http context accessor
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <returns></returns>
    /// <exception cref="SecurityTokenException"></exception>
    public static string GetToken(this IHttpContextAccessor httpContextAccessor)
    {
        var accessToken = httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
        if (!accessToken.HasValue) throw new SecurityTokenException(DefaultMessage.InvalidClaim);
        return accessToken.Value.ToString().Split(" ")[1];
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <returns></returns>
    public static string? GetIpAddress(this IHttpContextAccessor httpContextAccessor)
    {
        try
        {
            return httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }
        catch (Exception e)
        {
            return "0.0.0.0";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="claimType"></param>
    /// <returns></returns>
    /// <exception cref="SecurityTokenException"></exception>
    public static string GetClaim(this IHttpContextAccessor httpContextAccessor, string claimType)
    {
        var claim = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == claimType);
        if (claim is null) throw new SecurityTokenException(DefaultMessage.InvalidClaim);
        return claim.Value;
    }
}