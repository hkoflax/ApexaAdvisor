﻿using FluentAssertions;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Commands;

namespace AdvisorManager.Application.Tests.Requests
{
    public class CreateAdvisorRequestTests
    {
        [Fact]
        public void Constructor_ShouldInitializeDetails_WhenAdvisorDtoIsValid()
        {
            // Arrange
            var advisorDto = new AdvisorDto
            {
                Id = Guid.NewGuid(),
                FullName = "Test Advisor",
                SIN = "123456789",
                PhoneNumber = "1234567890",
                Address = "123 Main St",
                HealthStatus = "Green"
            };

            // Act
            var request = new CreateAdvisorRequest(advisorDto);

            // Assert
            request.Details.Should().NotBeNull();
            request.Details.Should().BeEquivalentTo(advisorDto);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenAdvisorDtoIsNull()
        {
            // Arrange
            AdvisorDto advisorDto = null;

            // Act
            Action act = () => new CreateAdvisorRequest(advisorDto);

            // Assert
            act.Should().Throw<ArgumentNullException>()
               .WithMessage("Value cannot be null. (Parameter 'advisorDto')");
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var advisorDto = new AdvisorDto
            {
                Id = Guid.NewGuid(),
                FullName = "Test Advisor",
                SIN = "123456789",
                PhoneNumber = "1234567890",
                Address = "123 Main St",
                HealthStatus = "Green"
            };
            var request = new CreateAdvisorRequest(advisorDto);

            // Act
            var result = request.ToString();

            // Assert
            result.Should().Contain("CreateAdvisorRequest");
            result.Should().Contain("RequestId");
            result.Should().Contain("Test Advisor");
        }
    }

}
