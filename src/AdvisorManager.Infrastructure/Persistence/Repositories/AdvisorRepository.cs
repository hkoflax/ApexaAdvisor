using AdvisorManager.Application;
using AdvisorManager.Domain;
using AdvisorManager.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdvisorManager.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implements the repository pattern for managing advisor entities in the database.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AdvisorRepository"/> class with the specified database context.
    /// </remarks>
    /// <param name="context">The <see cref="AdvisorContext"/> used to interact with the database.</param>
    public class AdvisorRepository(AdvisorContext context) : IAdvisorRepository
    {
        private readonly AdvisorContext _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <inheritdoc />
        public async Task<Advisor> GetByIdAsync(Guid id)
        {
            return await _context.Advisors.FindAsync(id).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<List<Advisor>> GetAllAsync() => await _context.Advisors.ToListAsync();

        /// <inheritdoc />
        public async Task<Advisor> AddAsync(Advisor advisor)
        {
            _context.Advisors.Add(advisor);
            await _context.SaveChangesAsync();
            return advisor;
        }

        /// <inheritdoc />
        public async Task<Advisor> UpdateAsync(Advisor advisor)
        {
            var trackedEntity = _context.Advisors.Local.FirstOrDefault(e => e.Id == advisor.Id);

            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).CurrentValues.SetValues(advisor);
            }
            else
            {
                _context.Advisors.Attach(advisor);
                _context.Entry(advisor).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return advisor;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Advisor advisor)
        {
            var trackedEntity = _context.Advisors.Local.FirstOrDefault(e => e.Id == advisor.Id);

            if (trackedEntity != null)
            {
                _context.Advisors.Remove(trackedEntity);
            }
            else
            {
                var toBeRemoved = new Advisor { Id = advisor.Id };
                _context.Advisors.Attach(toBeRemoved);
                _context.Advisors.Remove(toBeRemoved);
            }

            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<Advisor> GetBySINAsync(string sin)
        {
            return await _context.Advisors.FirstOrDefaultAsync(a => a.SIN == sin);
        }
    }
}
