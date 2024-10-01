using AdvisorManager.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdvisorManager.Infrastructure.Persistence.Contexts
{
    public class AdvisorContext(DbContextOptions<AdvisorContext> options) : DbContext(options)
    {
        public DbSet<Advisor> Advisors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advisor>()
                .HasIndex(a => a.SIN)
                .IsUnique();
        }
    }
}
