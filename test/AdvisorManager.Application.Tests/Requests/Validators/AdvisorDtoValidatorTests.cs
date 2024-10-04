using FluentValidation.TestHelper;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Application.Requests.Validators;

namespace AdvisorManager.Application.Tests.Requests.Validators
{
    public class AdvisorDtoValidatorTests
    {
        private readonly AdvisorDtoValidator _validator;

        public AdvisorDtoValidatorTests()
        {
            _validator = new AdvisorDtoValidator();
        }

        [Fact]
        public void Should_HaveError_When_FullNameIsEmpty()
        {
            // Arrange
            var model = new AdvisorDto { FullName = string.Empty };

            // Act & Assert
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(advisor => advisor.FullName)
                .WithErrorMessage("Full Name is required.");
        }

        [Fact]
        public void Should_HaveError_When_FullNameExceedsMaxLength()
        {
            // Arrange
            var model = new AdvisorDto { FullName = new string('a', 256) };

            // Act & Assert
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(advisor => advisor.FullName)
                .WithErrorMessage("Full Name can't exceed 255 characters.");
        }

        [Fact]
        public void Should_HaveError_When_SINIsNotValid()
        {
            // Arrange
            var model = new AdvisorDto { SIN = "123" };

            // Act & Assert
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(advisor => advisor.SIN)
                .WithErrorMessage("SIN must be exactly 9 digits.");
        }

        [Fact]
        public void Should_HaveError_When_SINIsNotDigits()
        {
            // Arrange
            var model = new AdvisorDto { SIN = "12345678a" };

            // Act & Assert
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(advisor => advisor.SIN)
                .WithErrorMessage("SIN must contain only digits.");
        }

        [Fact]
        public void Should_NotHaveError_When_SINIsValid()
        {
            // Arrange
            var model = new AdvisorDto { SIN = "123456789" };

            // Act & Assert
            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(advisor => advisor.SIN);
        }

        [Fact]
        public void Should_HaveError_When_AddressExceedsMaxLength()
        {
            // Arrange
            var model = new AdvisorDto { Address = new string('a', 256) };

            // Act & Assert
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(advisor => advisor.Address)
                .WithErrorMessage("Address can't exceed 255 characters.");
        }

        [Fact]
        public void Should_HaveError_When_PhoneNumberIsNot10Digits()
        {
            // Arrange
            var model = new AdvisorDto { PhoneNumber = "123" };

            // Act & Assert
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(advisor => advisor.PhoneNumber)
                .WithErrorMessage("Phone Number must be exactly 10 digits.");
        }

        [Fact]
        public void Should_HaveError_When_PhoneNumberIsNotDigits()
        {
            // Arrange
            var model = new AdvisorDto { PhoneNumber = "123456789a" };

            // Act & Assert
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(advisor => advisor.PhoneNumber)
                .WithErrorMessage("Phone Number must contain only digits.");
        }

        [Fact]
        public void Should_NotHaveError_When_PhoneNumberIsValid()
        {
            // Arrange
            var model = new AdvisorDto { PhoneNumber = "1234567890" };

            // Act & Assert
            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(advisor => advisor.PhoneNumber);
        }

        [Fact]
        public void Should_HaveError_When_HealthStatusIsInvalid()
        {
            // Arrange
            var model = new AdvisorDto { HealthStatus = "Invalid" };

            // Act & Assert
            _validator.TestValidate(model).ShouldHaveValidationErrorFor(advisor => advisor.HealthStatus)
                .WithErrorMessage("Health Status must be 'Green', 'Yellow', or 'Red'.");
        }

        [Fact]
        public void Should_NotHaveError_When_HealthStatusIsValid()
        {
            // Arrange
            var model = new AdvisorDto { HealthStatus = "Green" };

            // Act & Assert
            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(advisor => advisor.HealthStatus);
        }
    }

}
