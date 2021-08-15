using Pdam.Common.Shared.Fault;

namespace Pdam.Common.Shared.Http
{
    public class BaseResponse
    {
        internal bool IsSuccessful { get; set; } = true;
        internal int StatusCode { get; set; } = 200;
        public ErrorDetail Error { get; set; }
    }
}