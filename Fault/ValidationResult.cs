namespace Pdam.Common.Shared.Fault
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
            return new ValidationResult(false, message, null);
        }
    }
}