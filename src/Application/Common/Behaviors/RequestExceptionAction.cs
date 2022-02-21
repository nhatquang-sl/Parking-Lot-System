using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using PLS.Application.Common.Logging;

namespace PLS.Application.Common.Behaviors
{
    public class RequestExceptionAction<TRequest> : IRequestExceptionAction<TRequest>
    {
        private readonly ILogTrace _logTrace;
        public RequestExceptionAction(ILogTrace logTrace)
        {
            _logTrace = logTrace;
        }
        public async Task Execute(TRequest request, Exception exception, CancellationToken cancellationToken)
        {
            _logTrace.Error(exception);
        }
    }
}
