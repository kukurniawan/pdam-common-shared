using System.Threading.Tasks;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Fault
    // ReSharper restore CheckNamespace
{
    public interface IRequestValidator<in TRequest>
    {
        Task<ValidationResult> Validate(TRequest request);
        int Order { get; }
    }
}