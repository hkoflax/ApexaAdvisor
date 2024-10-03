using AdvisorManager.Domain;

namespace AdvisorManager.Application
{
    /// <summary>
    /// Represents the interface repository for managing advisor data.
    /// </summary>
    public interface IAdvisorRepository
    {
        /// <summary>
        /// Gets an advisor by the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the advisor.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains A <see cref="Advisor"/></returns>
        Task<Advisor> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets all advisors.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains A <see cref="List{T}" of <see cref="Advisor"/>/></returns>
        Task<List<Advisor>> GetAllAsync();

        /// <summary>
        /// Adds a new advisor.
        /// </summary>
        /// <param name="advisor">The advisor to <see langword="add"/>, a <see cref="Advisor"/></param>
        /// <returns>A task that represents the asynchronous operation. The task result contains A <see cref="Advisor"/></returns>
        Task<Advisor> AddAsync(Advisor advisor);

        /// <summary>
        /// Updates an existing advisor.
        /// </summary>
        /// <param name="advisor">The advisor to update, a <see cref="Advisor"/></param>
        /// <returns>A task that represents the asynchronous operation. The task result contains A <see cref="Advisor"/></returns>
        Task<Advisor> UpdateAsync(Advisor advisor);

        /// <summary>
        /// Deletes an advisor.
        /// </summary>
        /// <param name="advisor">The advisor to delete, A <see cref="Advisor"/></param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(Advisor advisor);

        /// <summary>
        /// Gets an advisor by the specified SIN (Social Insurance Number).
        /// </summary>
        /// <param name="sin">The Social Insurance Number of the advisor.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains A <see cref="Advisor"/></returns>
        Task<Advisor> GetBySINAsync(string sin);
    }
}
