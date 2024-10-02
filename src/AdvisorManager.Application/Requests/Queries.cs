using AdvisorManager.Application.Requests.Advisor.Queries;

namespace AdvisorManager.Application.Requests
{
    public static class Queries
    {
        public static GetAdvisorList GetAdvisorList()
            => new();
        public static GetAdvisorById GetAdvisorById(Guid advisorId)
            => new(advisorId);
        
    }
}
