using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Commands;

namespace AdvisorManager.Application.Requests
{
    public static class Commands
    {
        public static CreateAdvisorRequest CreateAdvisorRequest(AdvisorDto advisorDto)
            => new(advisorDto);

        public static UpdateAdvisorRequest UpdateAdvisorRequest(AdvisorDto advisorDto)
            => new(advisorDto);

        public static DeleteAdvisorRequest DeleteAdvisorRequest(Guid advisorId)
            => new(advisorId);
    }
}
