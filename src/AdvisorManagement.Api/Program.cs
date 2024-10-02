using AdvisorManagement.Api.DependencyInjection;
using AdvisorManagement.Api.Middlewares;
using AdvisorManager.Infrastructure.Persistence;
using AdvisorManager.Infrastructure.Persistence.Contexts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateBootstrapLogger();
try
{
    Log.Information("Starting API and configuring services");

    builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

	builder.Services.AddApplicationServices(builder.Configuration);

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

    app.ConfigureSerilog();
    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

    app.UseHttpsRedirection();

	app.UseAuthorization();

	app.MapControllers();

    if (app.Environment.IsDevelopment())
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AdvisorContext>();
            await Seed.SeedData(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred during Migration");
        }
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
