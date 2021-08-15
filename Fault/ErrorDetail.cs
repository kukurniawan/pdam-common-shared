using System.Net;

namespace Pdam.Common.Shared.Fault
{
    public class ErrorDetail
    {
        public string Description { get; set; }
        public int ErrorCode { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }
}