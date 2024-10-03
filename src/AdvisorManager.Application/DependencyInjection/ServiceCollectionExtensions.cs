using AdvisorManager.Application.Behaviors;
using AdvisorManager.Application.Mappings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AdvisorManager.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add all service in Application layer to the DI.
        /// </summary>
        /// <param name="services">THe current <see cref="IServiceCollection"/>.</param>
        /// <returns>The updated reference to the <paramref name="services"/> collection.</returns>
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddRequestValidators()
                           .AddApplicationBehaviours()
                           .AddModelMapping()
                           .AddMediatRHandlers();
                           
        }

        /// <summary>
        /// Add mapping profiles to the services collection.
        /// </summary>
        /// <param name="services">THe current <see cref="IServiceCollection"/>.</param>
        /// <returns>The updated reference to the <paramref name="services"/> collection.</returns>
        internal static IServiceCollection AddModelMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(opt => opt.AddProfiles(MappingProfiles.All));

            return services;
        }

        /// <summary>
        /// Adds the MediatR handlers to the services collection.
        /// </summary>
        /// <param name="services">THe current <see cref="IServiceCollection"/>.</param>
        /// <returns>The updated reference to the <paramref name="services"/> collection.</returns>
        internal static IServiceCollection AddMediatRHandlers(this IServiceCollection services)
            => services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        /// <summary>
        /// Adds the Fluent Validators to the services collection.
        /// </summary>
        /// <param name="services">THe current <see cref="IServiceCollection"/>.</param>
        /// <returns>The updated reference to the <paramref name="services"/> collection.</returns>
        internal static IServiceCollection AddRequestValidators(this IServiceCollection services)
            => services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        /// <summary>
        /// Adds behaviors to the services collection.
        /// </summary>
        /// <param name="services">THe current <see cref="IServiceCollection"/>.</param>
        /// <returns>The updated reference to the <paramref name="services"/> collection.</returns>
        internal static IServiceCollection AddApplicationBehaviours(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
