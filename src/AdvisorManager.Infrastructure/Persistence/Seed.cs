using AdvisorManager.Domain;
using AdvisorManager.Infrastructure.Persistence.Contexts;

namespace AdvisorManager.Infrastructure.Persistence
{
    /// <summary>
    /// Provides functionality to seed initial data into the <see cref="AdvisorContext"/>.
    /// </summary>
    public class Seed
    {
        /// <summary>
        /// Seeds initial advisor data into the database if no advisors are present.
        /// </summary>
        /// <param name="context">The <see cref="AdvisorContext"/> used to access the database.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of seeding data.</returns>
        public static async Task SeedData(AdvisorContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Advisors.Any())
            {
                var advisors = new List<Advisor> {
                new() { Address = "101 Main St", FullName = "Advisor 1", SIN = "123456789", PhoneNumber = "64712345678", HealthStatus = "Green" },
                new() { Address = "102 Main St", FullName = "Advisor 2", SIN = "223456789", PhoneNumber = "64712345679", HealthStatus = "Yellow" },
                new() { Address = "103 Main St", FullName = "Advisor 3", SIN = "323456789", PhoneNumber = "64712345680", HealthStatus = "Red" },
            };

                await context.Advisors.AddRangeAsync(advisors);
                await context.SaveChangesAsync();
            }
        }
    }

}