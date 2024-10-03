using AdvisorManager.Application.Requests.Advisor.Queries;

namespace AdvisorManager.Application.Requests
{
    public static class Queries
    {
        public static GetAdvisorListRequest GetAdvisorList()
            => new();
        public static GetAdvisorByIdRequest GetAdvisorById(Guid advisorId)
            => new(advisorId);
        
    }
}
