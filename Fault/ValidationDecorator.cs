using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Pdam.Common.Shared.Fault
{
    public class ValidationDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IRequestValidator<TRequest>> _validators;

        public ValidationDecorator(IEnumerable<IRequestValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any()) return await next();
            foreach (var validator in _validators.OrderBy(v => v.Order))
            {
                var result = await validator.Validate(request);

                if (!result.IsSuccessful)
                    throw new ApiException(HttpStatusCode.UnprocessableEntity, result.ErrorDescription, result.ErrorCode);
            }

            return await next();
        }
    }
}