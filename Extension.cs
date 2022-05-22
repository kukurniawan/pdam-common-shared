using Microsoft.AspNetCore.Builder;
using Pdam.Common.Shared.Infrastructure;

namespace Pdam.Common.Shared;

public static class Extension
{
    public static IApplicationBuilder UseFailureMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<FailureMiddleware>();
    }
}