using System;
using System.Net;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Fault
    // ReSharper restore CheckNamespace
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ApiException(HttpStatusCode status, string message, string resultErrorCode, string resultEventId = "0") : base(message)
        {
            StatusCode = status;
            ErrorCode = resultErrorCode;
            EventId = resultEventId;
        }

        public ApiException(HttpStatusCode status, string message, string resultErrorCode, Exception innerException, string resultEventId = "0") : base(message, innerException)
        {
            StatusCode = status;
            ErrorCode = resultErrorCode;
            EventId = resultEventId;

        }

        public string ErrorCode { get; set; }

        public ApiException(HttpStatusCode status, string message, string resultErrorCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = status;
            ErrorCode = resultErrorCode;
            EventId = "0";
        }
        
        public ApiException(ErrorDetail errorDetail) : base(errorDetail.Description)
        {
            StatusCode = errorDetail.StatusCode;
            ErrorCode = errorDetail.ErrorCode;
            EventId = "0";
        }
        
        public string EventId { get; set; }

    }
}