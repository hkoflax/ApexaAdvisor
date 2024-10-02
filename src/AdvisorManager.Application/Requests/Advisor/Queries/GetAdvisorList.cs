using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Models.Advisor;
using MediatR;

namespace AdvisorManager.Application.Requests.Advisor.Queries
{
    public class GetAdvisorList : RequestBase<AdvisorDto[]>, IRequest<Response<GetAdvisorList, AdvisorDto[]>>
    {
    }
}
