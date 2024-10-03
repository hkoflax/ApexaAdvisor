using Serilog;
using Serilog.Events;

namespace AdvisorManagement.Api.DependencyInjection
{
    /// <summary>
    /// An extension class of <see cref="IApplicationBuilder"/> to configure application middlewares.
    /// </summary>
    public static class ApplicationBuilderBuilderExtensions
    {
        /// <summary>
        /// Extension of <see cref="IApplicationBuilder"/> to configure serilog middleware.
        /// </summary>
        /// <param name="app">The current <see cref="IApplicationBuilder"/> to add the configuration to.</param>
        /// <returns>The updated <paramref name="app"/> reference.</returns>
        public static IApplicationBuilder ConfigureSerilog(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.GetLevel = (ctx, elapsed, ex) =>
                {
                    if (ex != null || ctx.Response.StatusCode > 499) return LogEventLevel.Error;
                    if (elapsed > TimeSpan.FromSeconds(3).TotalMilliseconds) return LogEventLevel.Warning;
                    return LogEventLevel.Information;
                };
            });

            return app;
        }

    }
}
