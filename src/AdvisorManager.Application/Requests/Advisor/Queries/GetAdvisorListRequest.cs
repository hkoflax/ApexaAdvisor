using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Models.Advisor;
using MediatR;

namespace AdvisorManager.Application.Requests.Advisor.Queries
{
    public class GetAdvisorListRequest : RequestBase<AdvisorDto[]>, IRequest<Response<GetAdvisorListRequest, AdvisorDto[]>>
    {
    }
}
