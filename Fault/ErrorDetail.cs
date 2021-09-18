using System.Net;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Fault
    // ReSharper restore CheckNamespace
{
    public class ErrorDetail
    {
        public string Description { get; set; }
        public string ErrorCode { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }
}