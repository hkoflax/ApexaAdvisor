using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Models.Advisor;
using MediatR;

namespace AdvisorManager.Application.Requests.Advisor.Queries
{
    public class GetAdvisorByIdRequest : RequestBase<AdvisorDto>, IRequest<Response<GetAdvisorByIdRequest, AdvisorDto>>
    {
        public GetAdvisorByIdRequest(Guid advisorId)
        {
            ArgumentNullException.ThrowIfNull(advisorId, nameof(advisorId));
            AdvisorId = advisorId;
        }

        public Guid AdvisorId { get; }
    }
}
