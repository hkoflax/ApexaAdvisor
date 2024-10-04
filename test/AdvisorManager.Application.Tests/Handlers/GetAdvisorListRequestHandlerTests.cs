using Moq;
using AutoMapper;
using FluentAssertions;
using AdvisorManager.Application.Handlers;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Queries;
using AdvisorManager.Domain;
namespace AdvisorManager.Application.Tests.Handlers
{
    public class GetAdvisorListRequestHandlerTests
    {
        private readonly Mock<IAdvisorRepository> _mockAdvisorRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAdvisorListRequestHandler _handler;

        public GetAdvisorListRequestHandlerTests()
        {
            _mockAdvisorRepository = new Mock<IAdvisorRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAdvisorListRequestHandler(_mockAdvisorRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAdvisorList_WhenAdvisorsExist()
        {
            // Arrange
            var advisors = new List<Advisor> {
            new Advisor { Id = Guid.NewGuid(), FullName = "Advisor 1", SIN = "123456789" },
            new Advisor { Id = Guid.NewGuid(), FullName = "Advisor 2", SIN = "987654321" }
        };

            var advisorDtos = new[] {
            new AdvisorDto { Id = advisors[0].Id, FullName = "Advisor 1", SIN = "123456789" },
            new AdvisorDto { Id = advisors[1].Id, FullName = "Advisor 2", SIN = "987654321" }
        };

            var request = new GetAdvisorListRequest();

            // Setup repository to return the list of advisors
            _mockAdvisorRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(advisors);

            // Setup mapper to map from Advisor[] to AdvisorDto[]
            _mockMapper.Setup(mapper => mapper.Map<AdvisorDto[]>(It.IsAny<List<Advisor>>()))
                .Returns(advisorDtos);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(advisorDtos);

            _mockAdvisorRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<AdvisorDto[]>(It.IsAny<List<Advisor>>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResponse_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new GetAdvisorListRequest();

            // Setup repository to throw an exception
            _mockAdvisorRepository.Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Test Exception"));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();
            result.Context.Exception.Message.Should().Contain("Test Exception");

            _mockAdvisorRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }

}
