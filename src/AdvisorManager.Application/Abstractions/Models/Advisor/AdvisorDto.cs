using System.ComponentModel.DataAnnotations;

namespace AdvisorManager.Application.Models.Advisor
{
    /// <summary>
    /// 
    /// </summary>
    public class AdvisorDto: IApplicationModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the advisor.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the full name of the advisor.
        /// </summary>
        [Required]
        [StringLength(255)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Social Insurance Number (SIN) of the advisor.
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "SIN must be exactly 9 digits.")]
        public string SIN { get; set; } = string.Empty ;

        /// <summary>
        /// Gets or sets the phone number of the advisor.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address of the advisor.
        /// </summary>
        [StringLength(255)]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the health status of the advisor.
        /// </summary>
        public string HealthStatus { get; set; } = string.Empty;
    }
}
