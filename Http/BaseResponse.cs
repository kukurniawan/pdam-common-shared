using Pdam.Common.Shared.Fault;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Http
    // ReSharper restore CheckNamespace
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSuccessful"></param>
        /// <param name="statusCode"></param>
        /// <param name="error"></param>
        public BaseResponse(bool isSuccessful, int statusCode, ErrorDetail error)
        {
            IsSuccessful = isSuccessful;
            StatusCode = statusCode;
            Error = error;
        }

        /// <summary>
        /// 
        /// </summary>
        protected BaseResponse()
        {
            IsSuccessful = true;
            StatusCode = 200;
            Error = new ErrorDetail();
        }

        internal bool IsSuccessful { get; set; }
        internal int StatusCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ErrorDetail Error { get; set; }
        public string Message { get; set; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public BaseResponse(T data)
        {
            Data = data;
            Message = "";
            StatusCode = 200;
            IsSuccessful = true;
        }
        
        public BaseResponse()
        {
            Data = default(T);
            Message = "";
            StatusCode = 200;
            IsSuccessful = true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <param name="status"></param>
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

        /// <summary>
        /// 
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }

        internal bool IsSuccessful { get; set; }
        internal int StatusCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ErrorDetail Error { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorDescription"></param>
        /// <param name="errorCode"></param>
        /// <param name="result"></param>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static BaseResponse<TValue> Fail<TValue>(string errorDescription, int errorCode = 0, TValue result = default!)
        {
            return new BaseResponse<TValue>(result, errorDescription, errorCode, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static BaseResponse<TValue> Ok<TValue>(TValue obj)
        {
            return new BaseResponse<TValue>(obj, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static BaseResponse<TValue> Ok<TValue>(TValue obj, string message)
        {
            return new BaseResponse<TValue>(obj, true, message);
        }
    }
}