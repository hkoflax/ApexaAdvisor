using FluentValidation.TestHelper;
using AdvisorManager.Application.Requests.Advisor.Queries;
using AdvisorManager.Application.Requests.Validators;

namespace AdvisorManager.Application.Tests.Requests.Validators
{
    public class GetAdvisorByIdRequestValidatorTests
    {
        private readonly GetAdvisorByIdRequestValidator _validator;

        public GetAdvisorByIdRequestValidatorTests()
        {
            _validator = new GetAdvisorByIdRequestValidator();
        }

        [Fact]
        public void Should_HaveError_When_AdvisorIdIsEmpty()
        {
            // Arrange
            var request = new GetAdvisorByIdRequest(Guid.Empty);

            // Act & Assert
            _validator.TestValidate(request).ShouldHaveValidationErrorFor(c => c.AdvisorId)
                .WithErrorMessage("The 'AdvisorId' field must be a non-empty Guid.");
        }

        [Fact]
        public void Should_NotHaveError_When_AdvisorIdIsValid()
        {
            // Arrange
            var request = new GetAdvisorByIdRequest(Guid.NewGuid());

            // Act & Assert
            _validator.TestValidate(request).ShouldNotHaveValidationErrorFor(c => c.AdvisorId);
        }
    }

}
