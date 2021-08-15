using System;
using System.Net;

namespace Pdam.Common.Shared.Fault
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ApiException(HttpStatusCode status, string message, int resultErrorCode, string resultEventId = "0") : base(message)
        {
            StatusCode = status;
            ErrorCode = resultErrorCode;
            EventId = resultEventId;
        }

        public ApiException(HttpStatusCode status, string message, int resultErrorCode, Exception innerException, string resultEventId = "0") : base(message, innerException)
        {
            StatusCode = status;
            ErrorCode = resultErrorCode;
            EventId = resultEventId;

        }

        public int ErrorCode { get; set; }

        public ApiException(HttpStatusCode status, string message, int resultErrorCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = status;
            ErrorCode = resultErrorCode;
        }
        
        public string EventId { get; set; }

    }
}