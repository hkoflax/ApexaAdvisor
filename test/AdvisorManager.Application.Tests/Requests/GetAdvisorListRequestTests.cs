using FluentAssertions;
using AdvisorManager.Application.Requests.Advisor.Queries;

namespace AdvisorManager.Application.Tests.Requests
{
    public class GetAdvisorListRequestTests
    {
        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var request = new GetAdvisorListRequest();

            // Act
            var result = request.ToString();

            // Assert
            result.Should().Contain("GetAdvisorListRequest");
            result.Should().Contain("RequestId");
        }
    }

}
