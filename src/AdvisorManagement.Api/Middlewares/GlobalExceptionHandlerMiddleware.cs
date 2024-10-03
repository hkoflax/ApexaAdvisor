using Newtonsoft.Json;
using FluentValidation;


namespace AdvisorManagement.Api.Middlewares
{
    /// <summary>
    /// Middleware to catch and handle all uncaught exceptions, providing consistent error messages to the caller.
    /// </summary>
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">A <see cref="RequestDelegate"/> that represents the next middleware in the pipeline.</param>
        /// <param name="logger">A <see cref="ILogger{TCategoryName}"/> used to log details of the exceptions.</param>
        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware to handle the HTTP request and catches uncaught exceptions.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> representing the current HTTP request.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation of handling the request and potential exceptions.
        /// </returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "An unhandled validation exception occurred.");

                var errors = ex.Errors.Select(error => new { Property = error.PropertyName, Message = error.ErrorMessage });
                var jsonErrors = JsonConvert.SerializeObject(new { Errors = errors });

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(jsonErrors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An error occurred. Please try again later.");
            }
        }
    }

}