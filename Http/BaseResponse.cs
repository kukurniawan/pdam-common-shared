using Pdam.Common.Shared.Fault;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Http
    // ReSharper restore CheckNamespace
{
    public class BaseResponse
    {
        public BaseResponse(bool isSuccessful, int statusCode, ErrorDetail error)
        {
            IsSuccessful = isSuccessful;
            StatusCode = statusCode;
            Error = error;
        }

        protected BaseResponse()
        {
            IsSuccessful = true;
            StatusCode = 200;
            Error = new ErrorDetail();
        }

        internal bool IsSuccessful { get; set; }
        internal int StatusCode { get; set; }
        public ErrorDetail Error { get; set; }
    }
    
    public class BaseResponse<T>
    {
        public BaseResponse(T data)
        {
            Data = data;
            Message = "";
            StatusCode = 200;
            IsSuccessful = true;
        }
        
        public BaseResponse(T data, string message, int errorCode = 200, bool? status = true)
        {
            Data = data;
            Message = message;
            StatusCode = errorCode;
            IsSuccessful = status ?? false;
        }
        
        private BaseResponse(T data,  bool status)
        {
            Data = data;
            StatusCode = 200;
            IsSuccessful = status;
        }
        private BaseResponse(T data,  bool status, string message)
        {
            Data = data;
            StatusCode = 200;
            IsSuccessful = status;
            Message = message;
        }

        public string? Message { get; set; }

        public T Data { get; set; }

        internal bool IsSuccessful { get; set; }
        internal int StatusCode { get; set; }
        public ErrorDetail Error { get; set; }
        public static BaseResponse<TValue> Fail<TValue>(string errorDescription, int errorCode = 0, TValue result = default!)
        {
            return new BaseResponse<TValue>(result, errorDescription, errorCode, false);
        }

        public static BaseResponse<TValue> Ok<TValue>(TValue obj)
        {
            return new BaseResponse<TValue>(obj, true);
        }
        public static BaseResponse<TValue> Ok<TValue>(TValue obj, string message)
        {
            return new BaseResponse<TValue>(obj, true, message);
        }
    }
}