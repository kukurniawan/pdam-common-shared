using System.Net;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Fault
    // ReSharper restore CheckNamespace
{
    public class ErrorDetail
    {
        public ErrorDetail(string description, string errorCode, HttpStatusCode statusCode)
        {
            Description = description;
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
        public ErrorDetail()
        {
            Description = string.Empty;
            ErrorCode = "0";
            StatusCode = HttpStatusCode.OK;
        }

        public string Description { get; set; }
        public string ErrorCode { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public static ErrorDetail UnauthorizedCompany =>
            new(DefaultMessage.UnauthorizedCompany, "1701", HttpStatusCode.BadRequest);
    }
}