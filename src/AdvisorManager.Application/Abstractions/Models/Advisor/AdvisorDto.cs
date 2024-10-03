namespace AdvisorManager.Application.Models.Advisor
{
    public class AdvisorDto: IApplicationModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string SIN { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string HealthStatus { get; set; } = string.Empty;
    }
}
