using AdvisorManagement.Api.Mappings;
using AdvisorManager.Application.DependencyInjection;
using AdvisorManager.Infrastructure.DependencyInjection;

namespace AdvisorManagement.Api.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddInfrastructure(configuration);
            services.AddApplicationLayer(configuration);

            services.AddApiModelMapping()
                    .AddAutoMapper(opt => opt.AddProfiles(MappingProfiles.ApiProfiles));

            return services;
        }

        public static IServiceCollection AddApiModelMapping(this IServiceCollection services)
        {
            services.AddTransient(typeof(IApiModelMapper<,>), typeof(DefaultApiModelMapper<,>));
            return services;
        }
    }
}
