using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Models.Advisor;
using MediatR;
using Newtonsoft.Json;

namespace AdvisorManager.Application.Requests.Advisor.Commands
{
    /// <summary>
    /// Represents a request to delete an advisor.
    /// </summary>
    /// <remarks>
    /// This request is used in the MediatR pipeline to handle operations related to deleting an advisor by their ID.
    /// </remarks>
    public class DeleteAdvisorRequest : RequestBase<AdvisorDto>, IRequest<Response<DeleteAdvisorRequest>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAdvisorRequest"/> class.
        /// </summary>
        /// <param name="advisorId">The unique identifier of the advisor to delete.</param>
        public DeleteAdvisorRequest(Guid advisorId)
        {
            AdvisorId = advisorId;
        }

        /// <summary>
        /// Gets the unique identifier of the advisor to be deleted.
        /// </summary>
        public Guid AdvisorId { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(DeleteAdvisorRequest)}(RequestId-{RequestId}) => {JsonConvert.SerializeObject(this, Formatting.Indented)}";
        }
    }

}
