using AdvisorManager.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdvisorManager.Infrastructure.Persistence.Contexts
{
    /// <summary>
    /// Represents the database context for managing advisor entities in the application.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AdvisorContext"/> class with the specified options.
    /// </remarks>
    /// <param name="options">The options to configure the database context.</param>
    public class AdvisorContext(DbContextOptions<AdvisorContext> options) : DbContext(options)
    {

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for advisors.
        /// </summary>
        public DbSet<Advisor> Advisors { get; set; }

        /// <summary>
        /// Configures the model for the context, specifically setting up a unique index on the SIN field for the <see cref="Advisor"/> entity.
        /// </summary>
        /// <param name="modelBuilder">The builder used to configure the entity models.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advisor>()
                .HasIndex(a => a.SIN)
                .IsUnique();
        }
    }

}
