using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Models.Advisor;
using MediatR;
using Newtonsoft.Json;

namespace AdvisorManager.Application.Requests.Advisor.Commands
{
    public class DeleteAdvisorRequest : RequestBase<AdvisorDto>, IRequest<Response<DeleteAdvisorRequest>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAdvisorRequest"/> class.
        /// </summary>
        /// <param name="advisorId">The Id of the advisor to delete</param>
        public DeleteAdvisorRequest(Guid advisorId)
        {
            AdvisorId = advisorId;
        }

        /// <summary>
        ///  Gets the question id to delete.
        /// </summary>
        public Guid AdvisorId { get; }


        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(DeleteAdvisorRequest)}(RequestId-{RequestId}) => {JsonConvert.SerializeObject(this, Formatting.Indented)}";
        }
    }
}
