using AdvisorManager.Domain;
using AdvisorManager.Infrastructure.Persistence.Contexts;

namespace AdvisorManager.Infrastructure.Persistence
{
    public class Seed
    {
        public static async Task SeedData(AdvisorContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            if (!context.Advisors.Any())
            {
                var advisors = new List<Advisor> {
                    new() { Address ="101 MAin str", FullName="Advisor 1", SIN="123456789", PhoneNumber="64712345678", HealthStatus="Green"},
                    new() { Address ="102 MAin str", FullName="Advisor 2", SIN="223456789", PhoneNumber="64712345679", HealthStatus="Yellow"},
                    new() { Address ="103 MAin str", FullName="Advisor 3", SIN="323456789", PhoneNumber="64712345680", HealthStatus="Red"},
                };

                await context.Advisors.AddRangeAsync(advisors);
                await context.SaveChangesAsync();
            }
        }
    }
}