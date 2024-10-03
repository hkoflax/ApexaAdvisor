using AdvisorManager.Application;
using AdvisorManager.Infrastructure.Persistence.Contexts;
using AdvisorManager.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvisorManager.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for setting up application infrastructure services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds infrastructure services to the application's service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configuration">The application's configuration settings.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            return services;
        }

        /// <summary>
        /// Adds persistence services, including database context and repositories, to the application's service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configuration">The application's configuration settings.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdvisorContext>(options => options.UseInMemoryDatabase("AdvisorsDb"));
            services.AddScoped<IAdvisorRepository, AdvisorRepository>();
            return services;
        }
    }

}
