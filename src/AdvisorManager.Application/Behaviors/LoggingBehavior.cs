using MediatR;
using Microsoft.Extensions.Logging;

namespace AdvisorManager.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// Initialize a <see cref="LoggingBehavior{TRequest, TResponse}"/>.
        /// </summary>
        /// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> of <see cref="LoggingBehavior{TRequest, TResponse}"/>.</param>
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation(request.ToString());

            var response = await next();

            _logger.LogInformation($"Handled: {response}");
            return response;
        }
    }
}