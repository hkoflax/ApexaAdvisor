using AdvisorManager.Application.Requests.Advisor.Queries;

namespace AdvisorManager.Application.Requests
{
    /// <summary>
    /// Provides factory methods for creating various query requests related to advisor operations.
    /// </summary>
    public static class Queries
    {
        /// <summary>
        /// Creates a new <see cref="GetAdvisorListRequest"/> to retrieve the list of advisors.
        /// </summary>
        /// <returns>A new instance of <see cref="GetAdvisorListRequest"/>.</returns>
        public static GetAdvisorListRequest GetAdvisorList()
            => new();

        /// <summary>
        /// Creates a new <see cref="GetAdvisorByIdRequest"/> to retrieve an advisor by their unique identifier.
        /// </summary>
        /// <param name="advisorId">The unique identifier of the advisor to retrieve.</param>
        /// <returns>A new instance of <see cref="GetAdvisorByIdRequest"/>.</returns>
        public static GetAdvisorByIdRequest GetAdvisorById(Guid advisorId)
            => new(advisorId);
    }

}
