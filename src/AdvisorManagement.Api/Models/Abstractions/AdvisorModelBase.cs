using System.ComponentModel.DataAnnotations;

namespace AdvisorManagement.Api.Models
{
    /// <summary>
    /// A base class for advisors Api models
    /// </summary>
    public abstract class AdvisorModelBase : IApiModel
    {
        /// <summary>
        /// Gets or sets the full name of the advisor.
        /// </summary>
        [Required(ErrorMessage = "Full Name is required")]
        [MaxLength(255, ErrorMessage = "Full Name can't exceed 255 characters")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the advisor.
        /// </summary>
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone Number must be exactly 10 digits")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone Number must be exactly 10 digits")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the address of the advisor.
        /// </summary>
        [MaxLength(255)]
        public string Address { get; set; }
    }
}
