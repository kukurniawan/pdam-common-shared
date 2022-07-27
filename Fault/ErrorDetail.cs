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
            new(DefaultMessage.UnauthorizedCompany, "9001", HttpStatusCode.BadRequest);
        
        public static ErrorDetail NoActionArgument =>
            new(DefaultMessage.InvalidActionArgument, "9002", HttpStatusCode.UnprocessableEntity);
        public static ErrorDetail InvalidRequest =>
            new(DefaultMessage.InvalidRequest, "9003", HttpStatusCode.UnprocessableEntity);
        
        public static ErrorDetail InvalidAppRequest =>
            new(DefaultMessage.InvalidRequest, "9004", HttpStatusCode.UnprocessableEntity);
        
        public static ErrorDetail NoSecurityToken =>
            new(DefaultMessage.UnauthorizedAccount, "9005", HttpStatusCode.Unauthorized);
        public static ErrorDetail NoClaim =>
            new(DefaultMessage.NoClaim, "9006", HttpStatusCode.Unauthorized);

        public static ErrorDetail UnauthorizedUser =>
            new(DefaultMessage.UnauthorizedEmail, "9007", HttpStatusCode.Unauthorized);
        public static ErrorDetail ErrorOnUpdateDbContext =>
            new(DefaultMessage.DEFAULT_ERROR_ON_INSERT_OR_UPDATE, "9008", HttpStatusCode.UnprocessableEntity);
    }
}