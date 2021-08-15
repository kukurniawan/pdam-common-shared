using System.Threading.Tasks;

namespace Pdam.Common.Shared.Fault
{
    public interface IRequestValidator<in TRequest>
    {
        Task<ValidationResult> Validate(TRequest request);
        int Order { get; }
    }
}