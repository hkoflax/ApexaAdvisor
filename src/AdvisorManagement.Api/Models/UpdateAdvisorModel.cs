using System.ComponentModel.DataAnnotations;

namespace AdvisorManagement.Api.Models
{
    /// <summary>
    /// A class representing an update advisor model.
    /// </summary>
    public class UpdateAdvisorModel : AdvisorModelBase
    {
        /// <summary>
        /// Gets or Sets the require Unique Id for the advisor to update.
        /// </summary>
        [Required]
        public Guid Id { get; set; }
    }
}
