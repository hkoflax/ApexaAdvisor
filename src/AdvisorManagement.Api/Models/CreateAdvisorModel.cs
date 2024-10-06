using System.ComponentModel.DataAnnotations;

namespace AdvisorManagement.Api.Models
{
    /// <summary>
    /// A class representing a create advisor model.
    /// </summary>
    public class CreateAdvisorModel: AdvisorModelBase
    {
        /// <summary>
        /// Gets or sets the Social Insurance Number (SIN) of the advisor.
        /// </summary>
        /// <example>123456789</example>
        [Required(ErrorMessage = "SIN is required")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "SIN must be exactly 9 characters")]
        [RegularExpression(@"\d{9}", ErrorMessage = "SIN must be exactly 9 digits")]
        public string SIN { get; set; }
    }
}
