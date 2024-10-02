using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Models.Advisor;
using MediatR;

namespace AdvisorManager.Application.Requests.Advisor.Queries
{
    public class GetAdvisorById : RequestBase<AdvisorDto>, IRequest<Response<GetAdvisorById, AdvisorDto>>
    {
        public GetAdvisorById(Guid advisorId)
        {
            ArgumentNullException.ThrowIfNull(advisorId, nameof(advisorId));
            AdvisorId = advisorId;
        }

        public Guid AdvisorId { get; }
    }
}
