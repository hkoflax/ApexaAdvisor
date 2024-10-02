using AdvisorManager.Application;
using AdvisorManager.Infrastructure.Persistence.Contexts;
using AdvisorManager.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvisorManager.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdvisorContext>(options => options.UseInMemoryDatabase("AdvisorsDb"));
            services.AddScoped<IAdvisorRepository, AdvisorRepository>();        
            return services;
        }
    }
}
