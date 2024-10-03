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
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the advisor.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the Social Insurance Number (SIN) of the advisor.
        /// </summary>
        public string SIN { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the advisor.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the address of the advisor.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the health status of the advisor.
        /// </summary>
        public string HealthStatus { get; set; } = string.Empty;
    }
}
