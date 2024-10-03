using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Commands;

namespace AdvisorManager.Application.Requests
{
    /// <summary>
    /// Provides factory methods for creating various command requests related to advisor operations.
    /// </summary>
    public static class Commands
    {
        /// <summary>
        /// Creates a new <see cref="CreateAdvisorRequest"/> to add an advisor.
        /// </summary>
        /// <param name="advisorDto">The advisor details for creating a new advisor.</param>
        /// <returns>A new instance of <see cref="CreateAdvisorRequest"/>.</returns>
        public static CreateAdvisorRequest CreateAdvisorRequest(AdvisorDto advisorDto)
            => new(advisorDto);

        /// <summary>
        /// Creates a new <see cref="UpdateAdvisorRequest"/> to update an advisor's details.
        /// </summary>
        /// <param name="advisorDto">The advisor details to update.</param>
        /// <returns>A new instance of <see cref="UpdateAdvisorRequest"/>.</returns>
        public static UpdateAdvisorRequest UpdateAdvisorRequest(AdvisorDto advisorDto)
            => new(advisorDto);

        /// <summary>
        /// Creates a new <see cref="DeleteAdvisorRequest"/> to delete an advisor by their ID.
        /// </summary>
        /// <param name="advisorId">The unique identifier of the advisor to delete.</param>
        /// <returns>A new instance of <see cref="DeleteAdvisorRequest"/>.</returns>
        public static DeleteAdvisorRequest DeleteAdvisorRequest(Guid advisorId)
            => new(advisorId);
    }

}
