using Moq;
using AutoMapper;
using FluentAssertions;
using AdvisorManager.Application.Handlers;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Queries;
using AdvisorManager.Domain;

namespace AdvisorManager.Application.Tests.Handlers
{
    public class GetAdvisorByIdRequestHandlerTests
    {
        private readonly Mock<IAdvisorRepository> _mockAdvisorRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAdvisorByIdRequestHandler _handler;

        public GetAdvisorByIdRequestHandlerTests()
        {
            _mockAdvisorRepository = new Mock<IAdvisorRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAdvisorByIdRequestHandler(_mockAdvisorRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAdvisor_WhenAdvisorExists()
        {
            // Arrange
            var advisorId = Guid.NewGuid();
            var advisor = new Advisor { Id = advisorId, FullName = "Test Advisor" };
            var advisorDto = new AdvisorDto { Id = advisorId, FullName = "Test Advisor" };
            var request = new GetAdvisorByIdRequest(advisorId);

            // Setup repository to return the advisor by ID
            _mockAdvisorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(advisor);

            // Setup mapper to map from Advisor to AdvisorDto
            _mockMapper.Setup(mapper => mapper.Map<AdvisorDto>(It.IsAny<Advisor>()))
                .Returns(advisorDto);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(advisorDto);

            _mockAdvisorRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<AdvisorDto>(It.IsAny<Advisor>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResponse_WhenAdvisorDoesNotExist()
        {
            // Arrange
            var advisorId = Guid.NewGuid();
            var request = new GetAdvisorByIdRequest(advisorId);

            // Setup repository to return null when advisor is not found
            _mockAdvisorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Advisor)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();

            _mockAdvisorRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<AdvisorDto>(It.IsAny<Advisor>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResponse_WhenExceptionIsThrown()
        {
            // Arrange
            var advisorId = Guid.NewGuid();
            var request = new GetAdvisorByIdRequest(advisorId);

            // Setup repository to throw an exception
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
