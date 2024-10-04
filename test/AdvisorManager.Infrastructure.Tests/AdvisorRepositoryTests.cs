using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using AdvisorManager.Infrastructure.Persistence.Contexts;
using AdvisorManager.Infrastructure.Persistence.Repositories;
using AdvisorManager.Domain;

namespace AdvisorManager.Infrastructure.Tests
{
    public class AdvisorRepositoryTests
    {
        private readonly AdvisorContext _context;
        private readonly AdvisorRepository _repository;

        public AdvisorRepositoryTests()
        {
            // Setup in-memory database
            var options = new DbContextOptionsBuilder<AdvisorContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AdvisorContext(options);
            _repository = new AdvisorRepository(_context);

            // Seed data for testing
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var advisors = new List<Advisor>
        {
            new Advisor { Id = Guid.NewGuid(), FullName = "Advisor 1", SIN = "123456789" },
            new Advisor { Id = Guid.NewGuid(), FullName = "Advisor 2", SIN = "987654321" }
        };

            _context.Advisors.AddRange(advisors);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAdvisor_WhenAdvisorExists()
        {
            // Arrange
            var advisor = await _context.Advisors.FirstAsync();

            // Act
            var result = await _repository.GetByIdAsync(advisor.Id);

            // Assert
            result.Should().NotBeNull();
            result.FullName.Should().Be(advisor.FullName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenAdvisorDoesNotExist()
        {
            // Act
            var result = await _repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllAdvisors()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task AddAsync_ShouldAddNewAdvisor()
        {
            // Arrange
            var newAdvisor = new Advisor { Id = Guid.NewGuid(), FullName = "Advisor 3", SIN = "111222333" };

            // Act
            var result = await _repository.AddAsync(newAdvisor);

            // Assert
            result.Should().NotBeNull();
            result.FullName.Should().Be("Advisor 3");

            var allAdvisors = await _repository.GetAllAsync();
            allAdvisors.Should().HaveCount(3);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAdvisor()
        {
            // Arrange
            var advisor = await _context.Advisors.FirstAsync();
            advisor.FullName = "Updated Advisor";

            // Act
            var result = await _repository.UpdateAsync(advisor);

            // Assert
            result.Should().NotBeNull();
            result.FullName.Should().Be("Updated Advisor");

            var updatedAdvisor = await _repository.GetByIdAsync(advisor.Id);
            updatedAdvisor.FullName.Should().Be("Updated Advisor");
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveAdvisor()
        {
            // Arrange
            var advisor = await _context.Advisors.FirstAsync();

            // Act
            await _repository.DeleteAsync(advisor);

            // Assert
            var allAdvisors = await _repository.GetAllAsync();
            allAdvisors.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetBySINAsync_ShouldReturnAdvisor_WhenSINExists()
        {
            // Act
            var result = await _repository.GetBySINAsync("123456789");

            // Assert
            result.Should().NotBeNull();
            result.FullName.Should().Be("Advisor 1");
        }

        [Fact]
        public async Task GetBySINAsync_ShouldReturnNull_WhenSINDoesNotExist()
        {
            // Act
            var result = await _repository.GetBySINAsync("000000000");

            // Assert
            result.Should().BeNull();
        }
    }

}
