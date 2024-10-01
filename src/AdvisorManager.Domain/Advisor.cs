using System.ComponentModel.DataAnnotations;

namespace AdvisorManager.Domain
{
    public class Advisor
    {
        public Guid Id { get; set; }

        [Required, MaxLength(255)]
        public string FullName { get; set; }

        [Required, StringLength(9)]
        public string SIN { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        public string HealthStatus { get; set; }
    }
}
