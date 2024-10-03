using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Models.Advisor;
using MediatR;
using Newtonsoft.Json;

namespace AdvisorManager.Application.Requests.Advisor.Commands
{
    /// <summary>
    /// Represents a request to create a new advisor.
    /// </summary>
    public class CreateAdvisorRequest : RequestBase<AdvisorDto>, IRequest<Response<CreateAdvisorRequest, AdvisorDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAdvisorRequest"/> class.
        /// </summary>
        /// <param name="advisorDto">The advisor details for creating a new advisor.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="advisorDto"/> is null.</exception>
        public CreateAdvisorRequest(AdvisorDto advisorDto)
        {
            ArgumentNullException.ThrowIfNull(advisorDto, nameof(advisorDto));
            Details = advisorDto;
        }

        /// <summary>
        /// Gets the details of the advisor to be created.
        /// </summary>
        public AdvisorDto Details { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(CreateAdvisorRequest)}(RequestId-{RequestId}) => {JsonConvert.SerializeObject(this, Formatting.Indented)}";
        }
    }
}
