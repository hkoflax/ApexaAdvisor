using MediatR;
using Microsoft.Extensions.Logging;

namespace AdvisorManager.Application.Behaviors
{

    /// <summary>
    /// A logging behavior that logs information about the request and response in the MediatR pipeline.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger used to log request and response details. 
        /// An instance of <see cref="ILogger{TCategoryName}"/> for <see cref="LoggingBehavior{TRequest, TResponse}"/>.
        /// </param>
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles logging of the request and response in the pipeline.
        /// </summary>
        /// <param name="request">The request being handled.</param>
        /// <param name="next">The delegate representing the next handler in the pipeline.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation, containing the response.</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation(request.ToString());

            var response = await next();

            _logger.LogInformation($"Handled: {response}");
            return response;
        }
    }
}