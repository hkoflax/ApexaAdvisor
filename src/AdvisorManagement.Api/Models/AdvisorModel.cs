namespace AdvisorManagement.Api.Models
{
    /// <summary>
    /// A class representing an Advisor.
    /// </summary>
    public class AdvisorModel: AdvisorModelBase, IApiModel
    {
        /// <summary>
        /// Gets or sets the unique Id of the advisor.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Health status of the advisor.
        /// </summary>
        public string HealthStatus { get; set; }

    }
}
