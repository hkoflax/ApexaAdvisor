using AdvisorManager.Application;
using AdvisorManager.Domain;
using AdvisorManager.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdvisorManager.Infrastructure.Persistence.Repositories
{
    public class AdvisorRepository: IAdvisorRepository
    {
        private readonly AdvisorContext _context;

        public AdvisorRepository(AdvisorContext context)
        {
            _context = context;
        }

        public async Task<Advisor> GetByIdAsync(Guid id)
        {
            return await _context.Advisors.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<List<Advisor>> GetAllAsync() => await _context.Advisors.ToListAsync();

        public async Task<Advisor> AddAsync(Advisor advisor)
        {
            _context.Advisors.Add(advisor);
            await _context.SaveChangesAsync();
            return advisor;
        }

        public async Task<Advisor> UpdateAsync(Advisor advisor)
        {
            _context.Advisors.Update(advisor);
            await _context.SaveChangesAsync();
            return advisor;
        }

        public async Task DeleteAsync(Advisor advisor)
        {
            _context.Advisors.Remove(advisor);
            await _context.SaveChangesAsync();
        }

        public async Task<Advisor> GetBySINAsync(string sin)
        {
            return await _context.Advisors.FirstOrDefaultAsync(a => a.SIN == sin);
        }
    }
}
