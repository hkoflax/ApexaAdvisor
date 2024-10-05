using AdvisorManagement.Blazor.UI.Components;
using Microsoft.Extensions.Configuration;
using AdvisorManager.Application.DependencyInjection;
using AdvisorManager.Infrastructure.DependencyInjection;
using AdvisorManager.Infrastructure.Persistence.Contexts;
using AdvisorManager.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
.AddInteractiveServerComponents();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

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
        Console.WriteLine(ex.Message, ";An error occurred during Migration");
    }
}

app.Run();
