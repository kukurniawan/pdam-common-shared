using FluentValidation;

namespace Pdam.Common.Shared.Fault;

public abstract class BaseValidator<T> : AbstractValidator<T>, IRequestValidator<T>
{
    protected BaseValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
    public new virtual async Task<ValidationResult> Validate(T request)
    {
        var result = await ValidateAsync(request);
        var errorCode = result.Errors.Select(x => x.ErrorCode).FirstOrDefault(x => x.Any());
        var errors = result.ToString(", ");
        if (result.Errors.Count > 1 || !short.TryParse(errorCode, out _))
        {
            errorCode = ErrorDetail.InvalidRequest.ErrorCode;
            errors = ErrorDetail.InvalidRequest.Description;
        }
        return result.IsValid ? ValidationResult.Ok() : ValidationResult.Error(new ErrorDetail(errors, errorCode, ErrorDetail.InvalidRequest.StatusCode));
    }

    public virtual int Order { get; } = 1;
}