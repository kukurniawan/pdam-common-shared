using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pdam.Common.Shared.Security;

namespace Pdam.Common.Shared.Http;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class HttpDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CookiesHelper _cookiesHelper;
    private readonly ApplicationSettings _webApplicationSettings;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="cookiesHelper"></param>
    /// <param name="webApplicationSettings"></param>
    public HttpDecorator(IHttpContextAccessor httpContextAccessor, CookiesHelper cookiesHelper, ApplicationSettings webApplicationSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _cookiesHelper = cookiesHelper;
        _webApplicationSettings = webApplicationSettings;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext == null) return await next();
        if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization")) return await next();
        var jwtToken = await _cookiesHelper.GetValue(_webApplicationSettings.JwtCookieName);
        if (!string.IsNullOrEmpty(jwtToken))
            _httpContextAccessor.HttpContext.Request.Headers.Add("Authorization",
                $"Bearer {jwtToken}");
        return await next();
    }
}