using System.Net;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Fault
    // ReSharper restore CheckNamespace
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorDetail
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <param name="errorCode"></param>
        /// <param name="statusCode"></param>
        public ErrorDetail(string description, string errorCode, HttpStatusCode statusCode)
        {
            Description = description;
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
        /// <summary>
        /// 
        /// </summary>
        public ErrorDetail()
        {
            Description = string.Empty;
            ErrorCode = "0";
            StatusCode = HttpStatusCode.OK;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static ErrorDetail UnauthorizedCompany =>
            new(DefaultMessage.UnauthorizedCompany, "9001", HttpStatusCode.BadRequest);
        
        /// <summary>
        /// 
        /// </summary>
        public static ErrorDetail InvalidActionState =>
            new(DefaultMessage.InvalidActionArgument, "9002", HttpStatusCode.UnprocessableEntity);
        /// <summary>
        /// 
        /// </summary>
        public static ErrorDetail InvalidRequest =>
            new(DefaultMessage.InvalidRequest, "9003", HttpStatusCode.UnprocessableEntity);
        
        /// <summary>
        /// 
        /// </summary>
        public static ErrorDetail InvalidAppRequest =>
            new(DefaultMessage.InvalidRequest, "9004", HttpStatusCode.UnprocessableEntity);
        
        /// <summary>
        /// 
        /// </summary>
        public static ErrorDetail NoSecurityToken =>
            new(DefaultMessage.UnauthorizedAccount, "9005", HttpStatusCode.Unauthorized);
        /// <summary>
        /// 
        /// </summary>
        public static ErrorDetail NoClaim =>
            new(DefaultMessage.NoClaim, "9006", HttpStatusCode.Unauthorized);

        /// <summary>
        /// 
        /// </summary>
        public static ErrorDetail UnauthorizedUser =>
            new(DefaultMessage.UnauthorizedEmail, "9007", HttpStatusCode.Unauthorized);
        /// <summary>
        /// 
        /// </summary>
        public static ErrorDetail ErrorOnUpdateDbContext =>
            new(DefaultMessage.DEFAULT_ERROR_ON_INSERT_OR_UPDATE, "9008", HttpStatusCode.UnprocessableEntity);
        
    }
}