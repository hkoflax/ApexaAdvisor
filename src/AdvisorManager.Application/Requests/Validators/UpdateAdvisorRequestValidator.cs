using AdvisorManager.Application.Requests.Advisor.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvisorManager.Application.Requests.Validators
{
    /// <summary>
    /// Validator for <see cref="UpdateAdvisorRequest"/> to ensure the request contains valid advisor details for updating.
    /// </summary>
    public class UpdateAdvisorRequestValidator : AbstractValidator<UpdateAdvisorRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAdvisorRequestValidator"/> class.
        /// </summary>
        public UpdateAdvisorRequestValidator()
        {
            RuleFor(c => c.Details)
                .NotNull()
                .WithMessage("Cannot use a null or empty object for create or update.");

            RuleFor(c => c.Details)
                .SetValidator(c => new AdvisorDtoValidator());
        }
    }

}
