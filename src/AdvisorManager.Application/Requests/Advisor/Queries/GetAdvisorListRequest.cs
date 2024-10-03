using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Models.Advisor;
using MediatR;
using Newtonsoft.Json;

namespace AdvisorManager.Application.Requests.Advisor.Queries
{
    /// <summary>
    /// Represents a request to retrieve a list of advisors.
    /// </summary>
    public class GetAdvisorListRequest : RequestBase<AdvisorDto[]>, IRequest<Response<GetAdvisorListRequest, AdvisorDto[]>>
    {
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(GetAdvisorListRequest)}(RequestId-{RequestId}) => {JsonConvert.SerializeObject(this, Formatting.Indented)}";
        }
    }
}
