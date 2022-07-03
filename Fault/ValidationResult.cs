// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Fault
    // ReSharper restore CheckNamespace
{
    public class ValidationResult
    {
        private ValidationResult(bool isSuccessful, string errorDescription, string errorCode)
        {
            IsSuccessful = isSuccessful;
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }

        public string ErrorDescription { get; private set; }

        public string ErrorCode { get; private set; }

        public bool IsSuccessful { get; private set; }

        public static ValidationResult Ok()
        {
            return new ValidationResult(true, null, null);
        }

        public static ValidationResult Error(string message)
        {
            return new ValidationResult(false, message, "422");
        }
        
        public static ValidationResult Error(ErrorDetail errorDetail)
        {
            return new ValidationResult(false, errorDetail.Description, errorDetail.ErrorCode);
        }
    }
}