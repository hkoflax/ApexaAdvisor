using System.ComponentModel.DataAnnotations;

namespace AdvisorManager.Domain
{
    /// <summary>
    /// Represents an advisor with personal and contact information.
    /// </summary>
    public class Advisor
    {
        /// <summary>
        /// Gets or sets the unique identifier for the advisor.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the advisor.
        /// </summary>
        [Required, MaxLength(255)]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the Social Insurance Number (SIN) of the advisor.
        /// </summary>
        [Required, StringLength(9)]
        public string SIN { get; set; }

        /// <summary>
        /// Gets or sets the address of the advisor.
        /// </summary>
        [MaxLength(255)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the advisor.
        /// </summary>
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the health status of the advisor.
        /// </summary>
        public string HealthStatus { get; set; }
    }

}
