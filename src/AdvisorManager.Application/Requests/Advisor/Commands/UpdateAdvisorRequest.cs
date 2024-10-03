using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Models.Advisor;
using MediatR;
using Newtonsoft.Json;

namespace AdvisorManager.Application.Requests.Advisor.Commands
{
    /// <summary>
    /// Represents a request to update an advisor's details.
    /// </summary>
    /// <remarks>
    /// This request is used in the MediatR pipeline to handle operations related to updating advisor details.
    /// </remarks>
    public class UpdateAdvisorRequest : RequestBase<AdvisorDto>, IRequest<Response<UpdateAdvisorRequest, AdvisorDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAdvisorRequest"/> class.
        /// </summary>
        /// <param name="advisorDto">The advisor details to update.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="advisorDto"/> is null.</exception>
        public UpdateAdvisorRequest(AdvisorDto advisorDto)
        {
            ArgumentNullException.ThrowIfNull(advisorDto, nameof(advisorDto));
            Details = advisorDto;
        }

        /// <summary>
        /// Gets the details of the advisor to be updated.
        /// </summary>
        public AdvisorDto Details { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(UpdateAdvisorRequest)}(RequestId-{RequestId}) => {JsonConvert.SerializeObject(this, Formatting.Indented)}";
        }
    }

}