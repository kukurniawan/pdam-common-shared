using System;
using System.Net.Http;
using System.Threading.Tasks;
using Pdam.Common.Shared.Security;

namespace Pdam.Common.Shared.Http;

/// <summary>
/// 
/// </summary>
public interface IHttpService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="resource"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    Task<HttpServiceResult<T>> PostAsJson<T, T2>(Uri uri, T2 resource, Action<HttpRequestMessage>? action = null)
        where T : class;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    Task<HttpServiceResult<LoginResponse>> DoLogin(LoginRequest loginRequest, Action<HttpRequestMessage>? action = null);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="registerRequest"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    Task<HttpServiceResult<LoginResponse>> Register(RegisterRequest loginRequest, Action<HttpRequestMessage>? action = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="refreshTokenRequest"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    Task<HttpServiceResult<LoginResponse>> RefreshToken(RefreshTokenRequest refreshTokenRequest,
        Action<HttpRequestMessage>? action = null);
}