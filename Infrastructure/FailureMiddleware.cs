using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pdam.Common.Shared.Fault;
using Sentry;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Infrastructure
    // ReSharper restore CheckNamespace
{
    /// <summary>
    /// 
    /// </summary>
    public class FailureMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public FailureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public async Task Invoke(HttpContext context, ILogger<FailureMiddleware> logger)
        {
            var currentBody = context.Response.Body;
            await using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;
            ErrorDetail? error = null;
            try
            {
                await _next(context);
            }
            catch (DbUpdateConcurrencyException exception)
            {
                logger.LogError(exception.Message, exception, DefaultEventId.DbUpdateConcurrencyException);
                SentrySdk.CaptureException(exception);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                logger.LogError(unauthorizedAccessException, unauthorizedAccessException.Message);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                error = new ErrorDetail
                {
                    ErrorCode = HttpStatusCode.Unauthorized.ToString(),
                    Description = unauthorizedAccessException.Message,
                    StatusCode =  HttpStatusCode.Unauthorized
                };
                SentrySdk.CaptureException(unauthorizedAccessException);
            }
            catch (ApiException apiException)
            {
                logger.LogError(apiException, apiException.EventId);
                context.Response.StatusCode = (int)apiException.StatusCode;
                error = new ErrorDetail
                {
                    ErrorCode = apiException.ErrorCode,
                    Description = apiException.Message,
                    StatusCode =  apiException.StatusCode
                };
                SentrySdk.CaptureException(apiException);
            }
            catch (Exception e)
            {
                logger.LogCritical(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                error = new ErrorDetail
                {
                    ErrorCode = "500",
                    Description = DefaultMessage.ErrorMessage
                };
                SentrySdk.CaptureException(e);
            }
            context.Response.Body = currentBody;
            memoryStream.Seek(0, SeekOrigin.Begin);

            var readToEnd = await new StreamReader(memoryStream).ReadToEndAsync();

            if (context.Response.StatusCode == 401)
            {
                context.Response.ContentType = "application/json;charset=utf-8";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetail
                {
                    Description = DefaultMessage.UnauthorizedAccount,
                    ErrorCode = "401",
                    StatusCode = HttpStatusCode.Unauthorized
                }));
                return;
            }
            
            if ( string.Compare(context.Response.ContentType, "image/png", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                var buffer = memoryStream.ToArray();
                await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                return;
            }
            if (context.Response.StatusCode == 200 && !string.IsNullOrEmpty(context.Response.ContentType) && !context.Response.ContentType.Contains("application/json"))
            {
                await context.Response.WriteAsync(readToEnd);
                return;
            }

            if (error != null)
            {
                context.Response.ContentType = "application/json;charset=utf-8";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                return;
            }

            if (!string.IsNullOrEmpty(context.Response.ContentType) && context.Response.ContentType.Contains("image"))
            {
                var buffer = memoryStream.ToArray();
                await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                return;
            }

            if (!string.IsNullOrEmpty(context.Response.ContentType) && !context.Response.ContentType.Contains("application/json"))
            {
                await context.Response.WriteAsync(readToEnd);
                return;
            }
            //context.Response.ContentType = "text";
            await context.Response.WriteAsync(readToEnd);
        }
    }
}