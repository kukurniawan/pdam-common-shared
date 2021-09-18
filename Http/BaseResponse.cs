using Pdam.Common.Shared.Fault;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Http
    // ReSharper restore CheckNamespace
{
    public class BaseResponse
    {
        internal bool IsSuccessful { get; set; } = true;
        internal int StatusCode { get; set; } = 200;
        public ErrorDetail Error { get; set; }
    }
}