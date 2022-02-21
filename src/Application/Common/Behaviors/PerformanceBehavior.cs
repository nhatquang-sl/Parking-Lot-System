using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PLS.Application.Common.Logging;

namespace PLS.Application.Common.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogTrace _logTrace;

        public PerformanceBehavior(ILogTrace logTrace)
        {
            _timer = new Stopwatch();
            _logTrace = logTrace;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;

                _logTrace.Warning($"Long Running Request: {requestName} ({elapsedMilliseconds} milliseconds)");
            }

            return response;
        }
    }
}
