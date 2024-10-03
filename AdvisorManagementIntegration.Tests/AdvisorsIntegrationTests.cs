using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using AdvisorManager.Infrastructure.Persistence.Contexts;
using AdvisorManager.Infrastructure.Persistence;
using AdvisorManagement.Api.Models;
using System.Net.Http.Json;
using FluentAssertions;

namespace AdvisorManagementIntegration.Tests
{
    public class AdvisorsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private HttpClient _httpClient;

        public AdvisorsIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            // Arrange
            await CreateDatabaseAsync();

            // Act
            var response = await _httpClient.GetAsync("/api/Advisor/GetAdvisorsList1");
            var result = await response.Content.ReadFromJsonAsync<AdvisorModel>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private async Task CreateDatabaseAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            var db = scopedService.GetRequiredService<AdvisorContext>();

            await Seed.SeedData(db);
        }
    }
}
