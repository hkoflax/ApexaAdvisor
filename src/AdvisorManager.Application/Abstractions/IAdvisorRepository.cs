using AdvisorManager.Domain;

namespace AdvisorManager.Application.Abstractions
{
    public interface IAdvisorRepository
    {
        Task<Advisor> GetByIdAsync(Guid id);
        Task<List<Advisor>> GetAllAsync();
        Task<Advisor> AddAsync(Advisor advisor);
        Task<Advisor> UpdateAsync(Advisor advisor);
        Task DeleteAsync(Advisor advisor);
        Task<Advisor> GetBySINAsync(string sin);
    }
}
