namespace AdvisorManagement.Api.Models
{
    public class AdvisorModel: AdvisorModelBase, IApiModel
    {
        public Guid Id { get; set; }
        public string HealthStatus { get; set; }

    }
}
