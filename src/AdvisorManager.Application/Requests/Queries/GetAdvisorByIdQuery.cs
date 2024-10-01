using AdvisorManager.Application.DTOs;
using MediatR;

namespace AdvisorManager.Application.Requests.Queries
{
    public class GetAdvisorByIdQuery : IRequest<AdvisorDto>
    {
        public GetAdvisorByIdQuery(Guid advisorId)
        {
            ArgumentNullException.ThrowIfNull(advisorId, nameof(advisorId));
            AdvisorId = advisorId;
        }
        public Guid AdvisorId { get; }
    }
}
