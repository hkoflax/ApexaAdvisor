using AdvisorManager.Application.Models.Advisor;
using MediatR;

namespace AdvisorManager.Application.Requests.Advisor.Commands
{
    public class CreateAdvisorCommand : IRequest<AdvisorDto>
    {
        public CreateAdvisorCommand(AdvisorDto advisorDto)
        {
            ArgumentNullException.ThrowIfNull(advisorDto, nameof(advisorDto));
            Details = advisorDto;
        }

        public AdvisorDto Details { get; }
    }
}
