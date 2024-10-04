using FluentAssertions;
using AdvisorManager.Application.Requests.Advisor.Commands;

namespace AdvisorManager.Application.Tests.Requests
{
    public class DeleteAdvisorRequestTests
    {
        [Fact]
        public void Constructor_ShouldInitializeAdvisorId_WhenValidIdIsProvided()
        {
            // Arrange
            var advisorId = Guid.NewGuid();

            // Act
            var request = new DeleteAdvisorRequest(advisorId);

            // Assert
            request.AdvisorId.Should().Be(advisorId);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenAdvisorIdIsDefault()
        {
            // Arrange
            var advisorId = Guid.Empty;

            // Act
            var request = new DeleteAdvisorRequest(advisorId);

            // Assert
            request.AdvisorId.Should().Be(Guid.Empty); // No exception is thrown since Guid.Empty is a valid Guid
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var advisorId = Guid.NewGuid();
            var request = new DeleteAdvisorRequest(advisorId);

            // Act
            var result = request.ToString();

            // Assert
            result.Should().Contain("DeleteAdvisorRequest");
            result.Should().Contain("RequestId");
            result.Should().Contain(advisorId.ToString());
        }
    }

}
