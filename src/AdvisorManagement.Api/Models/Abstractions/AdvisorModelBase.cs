using System.ComponentModel.DataAnnotations;

namespace AdvisorManagement.Api.Models
{
    public abstract class AdvisorModelBase : IApiModel
    {
        [Required(ErrorMessage = "Full Name is required")]
        [MaxLength(255, ErrorMessage = "Full Name can't exceed 255 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "SIN is required")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "SIN must be exactly 9 characters")]
        [RegularExpression(@"\d{9}", ErrorMessage = "SIN must be exactly 9 digits")]
        public string SIN { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone Number must be exactly 10 digits")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone Number must be exactly 10 digits")]
        public string PhoneNumber { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }
    }
}
