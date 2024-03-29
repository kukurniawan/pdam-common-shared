﻿using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pdam.Common.Shared.Fault;
using Pdam.Common.Shared.Logging;

namespace Pdam.Common.Shared.Infrastructure
{
    public class FailureMiddleware
    {
        private readonly RequestDelegate _next;

        public FailureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IApiLogger logger)
        {
            var currentBody = context.Response.Body;


            await using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;
            ErrorDetail error = null;
            try
            {
                await _next(context);
            }
            catch (ApiException apiException)
            {
                logger.Exception(apiException, apiException.EventId);
                context.Response.StatusCode = (int)apiException.StatusCode;
                error = new ErrorDetail
                {
                    ErrorCode = apiException.ErrorCode,
                    Description = apiException.Message
                };
            }
            catch (Exception e)
            {
                logger.Exception(e);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                error = new ErrorDetail
                {
                    ErrorCode = (int)HttpStatusCode.InternalServerError,
                    Description = DefaultMessage.ErrorMessage
                };
            }

            var readToEnd = await new StreamReader(memoryStream).ReadToEndAsync();
            if ( string.Compare(context.Response.ContentType, "image/png", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                var buffer = memoryStream.ToArray();
                await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                return;
            }
            if (context.Response.StatusCode == 200 && context.Response.ContentType != null && !context.Response.ContentType.Contains("application/json"))
            {
                await context.Response.WriteAsync(readToEnd);
                return;
            }

            if (error != null)
            {
                context.Response.ContentType = "application/json;charset=utf-8";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
            else
            {
                await context.Response.WriteAsync(readToEnd);
            }
        }
    }
}