using Microsoft.AspNetCore.Builder;
using Pdam.Common.Shared.Infrastructure;

namespace Pdam.Common.Shared;

/// <summary>
/// 
/// </summary>
public static class Extension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseFailureMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<FailureMiddleware>();
    }
}