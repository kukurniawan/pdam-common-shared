using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Pdam.Common.Shared.Fault;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseValidator<T> : AbstractValidator<T>, IRequestValidator<T>
{
    /// <summary>
    /// 
    /// </summary>
    protected BaseValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    public virtual int Order { get; } = 1;
}