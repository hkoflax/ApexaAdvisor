using AdvisorManager.Application.Abstractions.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdvisorManagement.Api.Abstractions
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        private IMediator _mediator;

        /// <summary>
        /// Gets the Mediator, A<see cref="IMediator"/>.
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// Handles the request context and returns an appropriate 4xx response or throws an ApiException.
        /// </summary>
        /// <param name="context">The <see cref="RequestContext"/>.</param>
        /// <returns>A <see cref="ObjectResult"/>.</returns>
        /// <exception cref="ApiException">Thrown if the <seealso cref="RequestContext.Status"/> is <seealso cref="RequestStatus.Faulted"/> or <seealso cref="RequestStatus.Cancelled"/>.</exception>
        protected ObjectResult HandleResponse(RequestContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            if (context.Status == RequestStatus.Rejected && context.Exception is null)
            {
                return new NotFoundObjectResult(null);
            }

            if (context.Status == RequestStatus.Faulted)
            {
                throw context.Exception;
            }

            if (context.Status == RequestStatus.Cancelled)
            {
                throw new Exception("Unable to process the API request. An internal process was canceled or timed out.");
            }

            return BadRequest(context.Exception is null ? context.Exception : new { Errors = new[] { new { context.Exception.Message } } });
        }
    }
}
