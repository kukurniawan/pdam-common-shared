using Pdam.Common.Shared.Fault;
using Pdam.Mountala.Invoice.SambunganBaru.Features.Invoice;

namespace Pdam.Common.Shared.Http;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class HttpServiceResult<TValue>
{
    /// <summary>
    /// 
    /// </summary>
    public bool IsSuccessful { get; }
    /// <summary>
    /// 
    /// </summary>
    public bool IsFailure => !IsSuccessful;
    /// <summary>
    /// 
    /// </summary>
    public string? ErrorDescription { get; }
    /// <summary>
    /// 
    /// </summary>
    public TValue Value { get; }
    /// <summary>
    /// 
    /// </summary>
    public string? ErrorCode { get; }
    /// <summary>
    /// 
    /// </summary>
    public int HttpStatusCode { get; }

    internal HttpServiceResult(TValue value, bool isSuccessful, string? errorDescription, string? errorCode, int httpStatusCode)
    {
        Value = value;
        IsSuccessful = isSuccessful;
        ErrorDescription = errorDescription;
        ErrorCode = errorCode;
        HttpStatusCode = httpStatusCode;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="httpStatusCode"></param>
    /// <returns></returns>
    public static HttpServiceResult<TValue> Ok(TValue value, int httpStatusCode)
    {
        return new HttpServiceResult<TValue>(value, true, null, null, httpStatusCode);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="errorDescription"></param>
    /// <param name="errorCode"></param>
    /// <param name="httpStatusCode"></param>
    /// <returns></returns>
    public static HttpServiceResult<TValue?> Fail(string errorDescription, string errorCode, int httpStatusCode)
    {
        return new HttpServiceResult<TValue?>(default(TValue), false, errorDescription, errorCode, httpStatusCode);
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="errorDetail"></param>
    /// <param name="httpStatusCode"></param>
    /// <returns></returns>
    public static HttpServiceResult<TValue?> Fail(ErrorDetail errorDetail, int httpStatusCode)
    {
        return new HttpServiceResult<TValue?>(default(TValue), false, errorDetail.Description, errorDetail.ErrorCode, httpStatusCode);
    }
}