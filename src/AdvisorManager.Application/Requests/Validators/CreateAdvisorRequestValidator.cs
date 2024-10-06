using AdvisorManager.Application.Requests.Advisor.Commands;
using FluentValidation;

namespace AdvisorManager.Application.Requests.Validators
{
    /// <summary>
    /// Validator for <see cref="CreateAdvisorRequest"/> to ensure the request contains valid advisor details.
    /// </summary>
    public class CreateAdvisorRequestValidator : AbstractValidator<CreateAdvisorRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAdvisorRequestValidator"/> class.
        /// </summary>
        public CreateAdvisorRequestValidator()
        {
            RuleFor(c => c.Details)
                .NotNull()
                .WithMessage("Cannot use a null or empty object for create or update.");


            RuleFor(c => c.Details.SIN)
                .NotEmpty().WithMessage("SIN is required.")
                .Length(9).WithMessage("SIN must be exactly 9 digits.")
                .Matches(@"^\d{9}$").WithMessage("SIN must contain only digits.");

            RuleFor(c => c.Details)
                .SetValidator(c => new AdvisorDtoValidator());
        }
    }

}
