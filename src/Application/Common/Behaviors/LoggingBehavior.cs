using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using PLS.Application.Common.Logging;

namespace PLS.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogTrace _logTrace;

        public LoggingBehavior(ILogTrace logTrace)
        {
            _logTrace = logTrace;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _logTrace.Info("{@request}", request);
        }
    }
}
