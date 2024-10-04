namespace AdvisorManager.Application.Tests.Handlers
{
    using Moq;
    using Xunit;
    using AutoMapper;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using AdvisorManager.Application.Handlers;
    using AdvisorManager.Application.Models.Advisor;
    using AdvisorManager.Application.Requests.Advisor.Commands;
    using AdvisorManager.Domain;

    public class UpdateAdvisorRequestHandlerTests
    {
        private readonly Mock<IAdvisorRepository> _mockAdvisorRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateAdvisorRequestHandler _handler;

        public UpdateAdvisorRequestHandlerTests()
        {
            _mockAdvisorRepository = new Mock<IAdvisorRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateAdvisorRequestHandler(_mockAdvisorRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFaultedResponse_WhenAdvisorWithSameSINExists()
        {
            // Arrange
            var advisorId = Guid.NewGuid();
            var request = new UpdateAdvisorRequest(new AdvisorDto { Id = advisorId, SIN = "123456789" });
            var thisAdvisor = new Advisor { Id = advisorId, SIN = "123456789" };
            var existingAdvisor = new Advisor { Id = Guid.NewGuid(), SIN = "123456789" };

            _mockAdvisorRepository.Setup(repo => repo.GetByIdAsync(advisorId))
                .ReturnsAsync(thisAdvisor);

            _mockAdvisorRepository.Setup(repo => repo.GetBySINAsync("123456789"))
                .ReturnsAsync(existingAdvisor);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();

            _mockAdvisorRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockAdvisorRepository.Verify(repo => repo.GetBySINAsync(It.IsAny<string>()), Times.Once);
            _mockAdvisorRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Advisor>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResponse_WhenAdvisorDoesNotExist()
        {
            // Arrange
            var advisorId = Guid.NewGuid();
            var request = new UpdateAdvisorRequest(new AdvisorDto { Id = advisorId, SIN = "123456789" });

            _mockAdvisorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Advisor)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();

            _mockAdvisorRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockAdvisorRepository.Verify(repo => repo.GetBySINAsync(It.IsAny<string>()), Times.Never);
            _mockAdvisorRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Advisor>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldUpdateAdvisor_WhenValidRequest()
        {
            // Arrange
            var advisorId = Guid.NewGuid();
            var request = new UpdateAdvisorRequest(new AdvisorDto { Id = advisorId, SIN = "123456789", FullName = "Updated Advisor" });
            var thisAdvisor = new Advisor { Id = advisorId, SIN = "123456789", HealthStatus = "Green" };

            _mockAdvisorRepository.Setup(repo => repo.GetByIdAsync(advisorId))
                .ReturnsAsync(thisAdvisor);

            _mockAdvisorRepository.Setup(repo => repo.GetBySINAsync("123456789"))
                .ReturnsAsync(thisAdvisor);

            _mockMapper.Setup(mapper => mapper.Map<Advisor>(It.IsAny<AdvisorDto>()))
                .Returns(thisAdvisor);

            _mockMapper.Setup(mapper => mapper.Map<AdvisorDto>(It.IsAny<Advisor>()))
                .Returns(request.Details);

            _mockAdvisorRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Advisor>()))
                .ReturnsAsync(thisAdvisor);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(request.Details);

            _mockAdvisorRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockAdvisorRepository.Verify(repo => repo.GetBySINAsync(It.IsAny<string>()), Times.Once);
            _mockAdvisorRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Advisor>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResponse_WhenExceptionThrown()
        {
            // Arrange
            var advisorId = Guid.NewGuid();
            var request = new UpdateAdvisorRequest(new AdvisorDto { Id = advisorId, SIN = "123456789" });

            _mockAdvisorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Test Exception"));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();
            result.Context.Exception.Message.Should().Contain("Test Exception");

            _mockAdvisorRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }

}
