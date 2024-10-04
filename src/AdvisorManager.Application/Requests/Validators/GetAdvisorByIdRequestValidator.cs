using AdvisorManager.Application.Requests.Advisor.Queries;
using FluentValidation;

namespace AdvisorManager.Application.Requests.Validators
{
    /// <summary>
    /// Validator for <see cref="GetAdvisorByIdRequest"/> to ensure the request contains valid Id.
    /// </summary>
    public class GetAdvisorByIdRequestValidator: AbstractValidator<GetAdvisorByIdRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAdvisorByIdRequestValidator"/> class.
        /// </summary>
        public GetAdvisorByIdRequestValidator()
        {
            RuleFor(c => c.AdvisorId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("The 'AdvisorId' field must be a non-empty Guid.");
        }
    }
}
