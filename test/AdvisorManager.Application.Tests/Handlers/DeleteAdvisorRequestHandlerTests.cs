using Moq;
using FluentAssertions;
using AdvisorManager.Application.Handlers;
using AdvisorManager.Application.Requests.Advisor.Commands;
using AdvisorManager.Domain;

namespace AdvisorManager.Application.Tests.Handlers
{

    public class DeleteAdvisorRequestHandlerTests
    {
        private readonly Mock<IAdvisorRepository> _mockAdvisorRepository;
        private readonly DeleteAdvisorRequestHandler _handler;

        public DeleteAdvisorRequestHandlerTests()
        {
            _mockAdvisorRepository = new Mock<IAdvisorRepository>();
            _handler = new DeleteAdvisorRequestHandler(_mockAdvisorRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResponse_WhenAdvisorDoesNotExist()
        {
            // Arrange
            var request = new DeleteAdvisorRequest(Guid.NewGuid());

            // Setup repository to return null when advisor is not found
            _mockAdvisorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Advisor)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();

            _mockAdvisorRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockAdvisorRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Advisor>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldDeleteAdvisor_WhenAdvisorExists()
        {
            // Arrange
            var advisorId = Guid.NewGuid();
            var request = new DeleteAdvisorRequest(advisorId);
            var advisor = new Advisor { Id = advisorId };

            // Setup repository to return an existing advisor
            _mockAdvisorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(advisor);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();

            _mockAdvisorRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockAdvisorRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Advisor>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResponse_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new DeleteAdvisorRequest(Guid.NewGuid());

            // Setup repository to throw an exception when trying to delete
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
