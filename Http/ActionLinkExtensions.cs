using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Pdam.Common.Shared.Fault;
using Pdam.Common.Shared.Helper;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Http
    // ReSharper restore CheckNamespace
{
    public static class ActionLinkExtensions
    {
        public static string GetFullLink(this IHttpContextAccessor httpContextAccessor, string route, string key)
        {
            return $"{GetBaseLink(httpContextAccessor)}/{route}/{key}";
        }
        
        public static string GetBaseLink(this IHttpContextAccessor httpContextAccessor)
        {
            var baseUrl = GetForwardedHostHeader(httpContextAccessor);
            var protocol = GetForwardedProtoHeader(httpContextAccessor);
            return $"{protocol}://{baseUrl.RemoveTrailingSlash()}";
        }

        private static string GetForwardedHostHeader(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext != null)
                return httpContextAccessor.HttpContext != null &&
                       httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-Forwarded-Host", out var stringValues)
                    ? stringValues.FirstOrDefault()
                    : httpContextAccessor.HttpContext.Request.Host.ToString();
            throw new ApiException(HttpStatusCode.BadRequest, "Invalid header http request", "400");
        }

        private static string GetForwardedProtoHeader(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext != null)
                return httpContextAccessor.HttpContext != null &&
                       httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-Forwarded-Proto",
                           out var stringValues)
                    ? stringValues.FirstOrDefault()
                    : httpContextAccessor.HttpContext.Request.Scheme;
            throw new ApiException(HttpStatusCode.BadRequest, "Invalid header http request", "400");
        }
    }
}