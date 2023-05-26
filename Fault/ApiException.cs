using System;
using System.Net;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Fault
    // ReSharper restore CheckNamespace
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="resultErrorCode"></param>
        /// <param name="resultEventId"></param>
        public ApiException(HttpStatusCode status, string message, string resultErrorCode, string resultEventId = "0") : base(message)
        {
            StatusCode = status;
            ErrorCode = resultErrorCode;
            EventId = resultEventId;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="resultEventId"></param>
        public ApiException(string message, string resultEventId = "0") : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            ErrorCode = "400";
            EventId = resultEventId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="resultErrorCode"></param>
        /// <param name="innerException"></param>
        /// <param name="resultEventId"></param>
        public ApiException(HttpStatusCode status, string message, string resultErrorCode, Exception innerException, string resultEventId = "0") : base(message, innerException)
        {
            StatusCode = status;
            ErrorCode = resultErrorCode;
            EventId = resultEventId;

        }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="resultErrorCode"></param>
        /// <param name="innerException"></param>
        public ApiException(HttpStatusCode status, string message, string resultErrorCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = status;
            ErrorCode = resultErrorCode;
            EventId = "0";
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorDetail"></param>
        public ApiException(ErrorDetail errorDetail) : base(errorDetail.Description)
        {
            StatusCode = errorDetail.StatusCode;
            ErrorCode = errorDetail.ErrorCode;
            EventId = "0";
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string EventId { get; set; }

    }
}