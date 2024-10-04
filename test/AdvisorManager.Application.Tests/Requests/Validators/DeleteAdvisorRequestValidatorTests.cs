using FluentValidation.TestHelper;
using AdvisorManager.Application.Requests.Advisor.Commands;
using AdvisorManager.Application.Requests.Validators;

namespace AdvisorManager.Application.Tests.Requests.Validators
{
    public class DeleteAdvisorRequestValidatorTests
    {
        private readonly DeleteAdvisorRequestValidator _validator;

        public DeleteAdvisorRequestValidatorTests()
        {
            _validator = new DeleteAdvisorRequestValidator();
        }

        [Fact]
        public void Should_HaveError_When_AdvisorIdIsEmpty()
        {
            // Arrange
            var request = new DeleteAdvisorRequest(Guid.Empty);

            // Act & Assert
            _validator.TestValidate(request).ShouldHaveValidationErrorFor(c => c.AdvisorId)
                .WithErrorMessage("The 'AdvisorId' field must be a non-empty Guid.");
        }

        [Fact]
        public void Should_NotHaveError_When_AdvisorIdIsValid()
        {
            // Arrange
            var request = new DeleteAdvisorRequest(Guid.NewGuid());

            // Act & Assert
            _validator.TestValidate(request).ShouldNotHaveValidationErrorFor(c => c.AdvisorId);
        }

        [Fact]
        public void Should_HaveError_When_AdvisorIdIsNull()
        {
            // Arrange
            var request = new DeleteAdvisorRequest(Guid.Empty);

            // Act & Assert
            _validator.TestValidate(request).ShouldHaveValidationErrorFor(c => c.AdvisorId)
                .WithErrorMessage("The 'AdvisorId' field must be a non-empty Guid.");
        }
    }

}
