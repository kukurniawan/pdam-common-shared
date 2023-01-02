using Microsoft.AspNetCore.Mvc;
using Pdam.Common.Shared.Fault;
using Pdam.Common.Shared.Http;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Infrastructure
    // ReSharper restore CheckNamespace
{
    /// <summary>
    /// 
    /// </summary>
    public static class ActionResultMapper
    {
        private const string Message =
            "Something went wrong. Please try again in a few minutes or contact your support team.";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        public static IActionResult ToActionResult<TResponse>(TResponse result) where TResponse : BaseResponse
        {
            return result.IsSuccessful
                ? new ObjectResult(result)
                    { StatusCode = result.StatusCode }
                : new ObjectResult(result.Error)
                    { StatusCode = result.StatusCode };
        }
    }
}