using Moq;
using AutoMapper;
using FluentAssertions;
using AdvisorManager.Application.Handlers;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Commands;
using AdvisorManager.Domain;

namespace AdvisorManager.Application.Tests.Handlers
{
    public class CreateAdvisorRequestHandlerTests
    {
        private readonly Mock<IAdvisorRepository> _mockAdvisorRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateAdvisorRequestHandler _handler;

        public CreateAdvisorRequestHandlerTests()
        {
            _mockAdvisorRepository = new Mock<IAdvisorRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateAdvisorRequestHandler(_mockAdvisorRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFaultedResponse_WhenAdvisorWithSameSINExists()
        {
            // Arrange
            var request = new CreateAdvisorRequest(new AdvisorDto { SIN = "123456789", FullName = "Test Advisor" });

            _mockAdvisorRepository.Setup(repo => repo.GetBySINAsync("123456789"))
                .ReturnsAsync(new Advisor { SIN = "123456789" });

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();

            _mockAdvisorRepository.Verify(repo => repo.GetBySINAsync(It.IsAny<string>()), Times.Once);
            _mockAdvisorRepository.Verify(repo => repo.AddAsync(It.IsAny<Advisor>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldCreateAdvisor_WhenSINDoesNotExist()
        {
            // Arrange
            var request = new CreateAdvisorRequest(new AdvisorDto { SIN = "123456789", FullName = "New Advisor" });

            _mockAdvisorRepository.Setup(repo => repo.GetBySINAsync(It.IsAny<string>()))
                .ReturnsAsync((Advisor)null);

            _mockMapper.Setup(mapper => mapper.Map<Advisor>(It.IsAny<AdvisorDto>()))
                .Returns(new Advisor { SIN = "123456789", FullName = "New Advisor" });

            _mockMapper.Setup(mapper => mapper.Map<AdvisorDto>(It.IsAny<Advisor>()))
                .Returns(request.Details);

            _mockAdvisorRepository.Setup(repo => repo.AddAsync(It.IsAny<Advisor>()))
                .ReturnsAsync(new Advisor { SIN = "123456789", FullName = "New Advisor" });

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(request.Details);

            _mockAdvisorRepository.Verify(repo => repo.GetBySINAsync(It.IsAny<string>()), Times.Once);
            _mockAdvisorRepository.Verify(repo => repo.AddAsync(It.IsAny<Advisor>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<Advisor>(It.IsAny<AdvisorDto>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<AdvisorDto>(It.IsAny<Advisor>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResponse_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new CreateAdvisorRequest(new AdvisorDto { SIN = "123456789", FullName = "Test Advisor" });

            _mockAdvisorRepository.Setup(repo => repo.GetBySINAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Test Exception"));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse(); 
            result.Context.Exception.Message.Should().Contain("Test Exception");


            _mockAdvisorRepository.Verify(repo => repo.GetBySINAsync(It.IsAny<string>()), Times.Once);
        }
    }

}
