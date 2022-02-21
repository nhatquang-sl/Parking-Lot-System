using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using PLS.Application.Common.Exceptions;
using PLS.Application.Common.Logging;

namespace PLS.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogTrace _logTrace;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogTrace logTrace)
        {
            _validators = validators;
            _logTrace = logTrace;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    _logTrace.Warning(string.Join(", ", failures.Select(x => x.ErrorMessage)));
                    throw new BadRequestException(failures.First().ErrorMessage);
                }
            }

            return await next();
        }
    }
}
