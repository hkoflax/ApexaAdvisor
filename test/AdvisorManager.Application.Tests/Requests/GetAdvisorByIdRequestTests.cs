using FluentAssertions;
using AdvisorManager.Application.Requests.Advisor.Queries;

namespace AdvisorManager.Application.Tests.Requests
{
    public class GetAdvisorByIdRequestTests
    {
        [Fact]
        public void Constructor_ShouldInitializeAdvisorId_WhenValidIdIsProvided()
        {
            // Arrange
            var advisorId = Guid.NewGuid();

            // Act
            var request = new GetAdvisorByIdRequest(advisorId);

            // Assert
            request.AdvisorId.Should().Be(advisorId);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var advisorId = Guid.NewGuid();
            var request = new GetAdvisorByIdRequest(advisorId);

            // Act
            var result = request.ToString();

            // Assert
            result.Should().Contain("GetAdvisorByIdRequest");
            result.Should().Contain("RequestId");
            result.Should().Contain(advisorId.ToString());
        }
    }

}
