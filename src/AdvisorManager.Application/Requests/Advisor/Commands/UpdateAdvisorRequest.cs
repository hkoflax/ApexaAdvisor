using AdvisorManager.Application.Abstractions.Requests;
using AdvisorManager.Application.Models.Advisor;
using MediatR;

namespace AdvisorManager.Application.Requests.Advisor.Commands
{
    public class UpdateAdvisorRequest : RequestBase<AdvisorDto>, IRequest<Response<UpdateAdvisorRequest, AdvisorDto>>
    {
        public UpdateAdvisorRequest(AdvisorDto advisorDto)
        {
            ArgumentNullException.ThrowIfNull(advisorDto, nameof(advisorDto));
            Details = advisorDto;
        }

        public AdvisorDto Details { get; }
    }
}