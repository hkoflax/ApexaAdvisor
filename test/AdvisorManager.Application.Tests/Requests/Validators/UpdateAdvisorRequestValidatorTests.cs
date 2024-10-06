using FluentValidation.TestHelper;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Advisor.Commands;
using AdvisorManager.Application.Requests.Validators;

namespace AdvisorManager.Application.Tests.Requests.Validators
{
    public class UpdateAdvisorRequestValidatorTests
    {
        private readonly UpdateAdvisorRequestValidator _validator;

        public UpdateAdvisorRequestValidatorTests()
        {
            _validator = new UpdateAdvisorRequestValidator();
        }

        [Fact]
        public void Should_NotHaveError_When_DetailsIsValid()
        {
            // Arrange
            var advisorDto = new AdvisorDto
            {
                FullName = "Test Advisor",
                SIN = "123456789",
                PhoneNumber = "1234567890",
                Address = "123 Main St",
                HealthStatus = "Green"
            };
            var request = new UpdateAdvisorRequest(advisorDto);

            // Act & Assert
            _validator.TestValidate(request).ShouldNotHaveValidationErrorFor(c => c.Details);
        }

        [Fact]
        public void Should_UseAdvisorDtoValidator_When_DetailsIsSet()
        {
            // Arrange
            var invalidAdvisorDto = new AdvisorDto
            {
                FullName = string.Empty,  // Invalid FullName
                SIN = "123",             // Invalid SIN
                PhoneNumber = "abc"      // Invalid Phone Number
            };
            var request = new UpdateAdvisorRequest(invalidAdvisorDto);

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Details.FullName)
                .WithErrorMessage("Full Name is required.");
            result.ShouldHaveValidationErrorFor(c => c.Details.PhoneNumber)
                .WithErrorMessage("Phone Number must be exactly 10 digits.");
        }
    }

}
