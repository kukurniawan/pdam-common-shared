// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Fault
    // ReSharper restore CheckNamespace
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidationResult
    {
        private ValidationResult(bool isSuccessful, string errorDescription, string errorCode)
        {
            IsSuccessful = isSuccessful;
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorDescription { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccessful { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ValidationResult Ok()
        {
            return new ValidationResult(true, string.Empty, string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ValidationResult Error(string message)
        {
            return new ValidationResult(false, message, "422");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorDetail"></param>
        /// <returns></returns>
        public static ValidationResult Error(ErrorDetail errorDetail)
        {
            return new ValidationResult(false, errorDetail.Description, errorDetail.ErrorCode);
        }
    }
}