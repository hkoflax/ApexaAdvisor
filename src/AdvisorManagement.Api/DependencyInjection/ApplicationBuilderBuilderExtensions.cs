using Serilog;
using Serilog.Events;

namespace AdvisorManagement.Api.DependencyInjection
{
    public static class ApplicationBuilderBuilderExtensions
    {
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
