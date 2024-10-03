using AdvisorManagement.Api.Mappings;
using AdvisorManager.Application.DependencyInjection;
using AdvisorManager.Infrastructure.DependencyInjection;

namespace AdvisorManagement.Api.DependencyInjection
{
    /// <summary>
    /// An extension class to register services in the DI container
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register all services required by the App.
        /// </summary>
        /// <param name="services">The current <see cref="IServiceCollection"/> to add the service registrations to.</param>
        /// <param name="configuration">A <see cref="IConfiguration"/></param>
        /// <returns>The updated <paramref name="services"/> reference.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddInfrastructure(configuration);
            services.AddApplicationLayer(configuration);

            services.AddApiModelMapping();

            return services;
        }

        /// <summary>
        /// Adds the common API model mapping services, profiles and default value resolvers.
        /// </summary>
        /// <param name="services">The current <see cref="IServiceCollection"/> to add the service registrations to.</param>
        /// <returns>The updated <paramref name="services"/> reference.</returns>
        public static IServiceCollection AddApiModelMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(opt => opt.AddProfiles(MappingProfiles.ApiProfiles));
            services.AddTransient(typeof(IApiModelMapper<,>), typeof(DefaultApiModelMapper<,>));
            return services;
        }
    }
}
