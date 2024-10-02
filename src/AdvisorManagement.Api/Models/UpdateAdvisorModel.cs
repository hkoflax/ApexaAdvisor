using System.ComponentModel.DataAnnotations;

namespace AdvisorManagement.Api.Models
{
    public class UpdateAdvisorModel : AdvisorModelBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}
