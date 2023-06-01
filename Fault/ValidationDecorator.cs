using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Fault
    // ReSharper restore CheckNamespace
{
    /// <inheritdoc />
    public class ValidationDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IRequestValidator<TRequest>> _validators;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validators"></param>
        public ValidationDecorator(IEnumerable<IRequestValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="next"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any()) return await next();
            foreach (var validator in _validators.OrderBy(v => v.Order))
            {
                var result = await validator.Validate(request);

                if (!result.IsSuccessful)
                    throw new ApiException(HttpStatusCode.BadRequest, result.ErrorDescription, result.ErrorCode);
            }

            return await next();
        }
    }
}