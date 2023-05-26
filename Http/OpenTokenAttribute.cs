using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Pdam.Common.Shared.Fault;
using Microsoft.Extensions.Configuration;

namespace Pdam.Common.Shared.Http;

/// <summary>
/// 
/// </summary>
public class OpenTokenAttribute: ActionFilterAttribute
{
    private const string XApiTokenKey = "x-api-token";
    /// <summary>
    /// 
    /// </summary>
    public OpenTokenAttribute() 
    {
        this.Order = 1;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var reqHeader = context.HttpContext.Request.Headers;
        if (!reqHeader.ContainsKey(XApiTokenKey))
            throw new ApiException(ErrorDetail.UnauthorizedRequest);
        
        var apiToken = reqHeader[XApiTokenKey][0];
        if (string.IsNullOrEmpty(apiToken)) throw new ApiException(ErrorDetail.InvalidConfiguration);
        
        var configuration =(IConfiguration) context.HttpContext.RequestServices.GetService(typeof(IConfiguration))!;//context.HttpContext.RequestServices.GetService<IConfiguration>();
        if (configuration == null) throw new ApiException(ErrorDetail.InvalidConfiguration);
        
        var validToken = configuration.GetSection(XApiTokenKey).Value;
        if(!apiToken.Equals(validToken, StringComparison.OrdinalIgnoreCase))
            throw new ApiException(ErrorDetail.UnauthorizedRequest);
        base.OnActionExecuting(context);
           
    }
}