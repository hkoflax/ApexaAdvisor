using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Commands;
using MediatR;
using Newtonsoft.Json;

namespace AdvisorManager.Application.Requests.Advisor.Queries
{
    /// <summary>
    /// Represents a request to retrieve an advisor by their unique identifier.
    /// </summary>
    public class GetAdvisorByIdRequest : RequestBase<AdvisorDto>, IRequest<Response<GetAdvisorByIdRequest, AdvisorDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAdvisorByIdRequest"/> class.
        /// </summary>
        /// <param name="advisorId">The unique identifier of the advisor to retrieve.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="advisorId"/> is null.</exception>
        public GetAdvisorByIdRequest(Guid advisorId)
        {
            ArgumentNullException.ThrowIfNull(advisorId, nameof(advisorId));
            AdvisorId = advisorId;
        }

        /// <summary>
        /// Gets the unique identifier of the advisor.
        /// </summary>
        public Guid AdvisorId { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(GetAdvisorByIdRequest)}(RequestId-{RequestId}) => {JsonConvert.SerializeObject(this, Formatting.Indented)}";
        }
    }

}
