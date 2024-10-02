namespace AdvisorManager.Application.Abstractions.Requests
{
    public enum RequestStatus
    {
        Cancelled = -1,
        Completed = 0,
        Rejected = 1,
        Faulted = 2,
    }
}
