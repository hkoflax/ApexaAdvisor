using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Models.Advisor;
using MediatR;

namespace AdvisorManager.Application.Requests.Advisor.Commands
{
    public class CreateAdvisorRequest : RequestBase<AdvisorDto>, IRequest<Response<CreateAdvisorRequest, AdvisorDto>>
    {
        public CreateAdvisorRequest(AdvisorDto advisorDto)
        {
            ArgumentNullException.ThrowIfNull(advisorDto, nameof(advisorDto));
            Details = advisorDto;
        }

        public AdvisorDto Details { get; }
    }
}
