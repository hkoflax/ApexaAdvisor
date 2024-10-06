using AdvisorManager.Application.Models.Advisor;
using FluentValidation;

namespace AdvisorManager.Application.Requests.Validators
{
    /// <summary>
    /// Validator for <see cref="AdvisorDto"/> to ensure the properties contains valid details.
    /// </summary>
    public class AdvisorDtoValidator : AbstractValidator<AdvisorDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvisorDtoValidator"/> class.
        /// </summary>
        public AdvisorDtoValidator()
        {
            RuleFor(advisor => advisor.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MaximumLength(255).WithMessage("Full Name can't exceed 255 characters.");

            RuleFor(advisor => advisor.Address)
                .MaximumLength(255).WithMessage("Address can't exceed 255 characters.")
                .When(advisor => !string.IsNullOrWhiteSpace(advisor.Address));

            RuleFor(advisor => advisor.PhoneNumber)
                .Length(10).WithMessage("Phone Number must be exactly 10 digits.")
                .Matches(@"^\d{10}$").WithMessage("Phone Number must contain only digits.")
                .When(advisor => !string.IsNullOrWhiteSpace(advisor.PhoneNumber));

            RuleFor(advisor => advisor.HealthStatus)
                .Must(status => status == "Green" || status == "Yellow" || status == "Red")
                .WithMessage("Health Status must be 'Green', 'Yellow', or 'Red'.")
                .When(advisor => !string.IsNullOrWhiteSpace(advisor.HealthStatus));
        }
    }
}

