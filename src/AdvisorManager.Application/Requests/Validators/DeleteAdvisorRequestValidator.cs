using AdvisorManager.Application.Requests.Advisor.Commands;
using FluentValidation;

namespace AdvisorManager.Application.Requests.Validators
{
    /// <summary>
    /// Validator for <see cref="DeleteAdvisorRequest"/> to ensure the request contains valid Id.
    /// </summary>
    public class DeleteAdvisorRequestValidator: AbstractValidator<DeleteAdvisorRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAdvisorRequestValidator"/> class.
        /// </summary>
        public DeleteAdvisorRequestValidator()
        {
            RuleFor(c => c.AdvisorId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("The 'AdvisorId' field must be a non-empty Guid.");
        }
    }
}
