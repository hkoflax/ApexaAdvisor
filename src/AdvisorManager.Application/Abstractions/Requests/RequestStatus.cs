namespace AdvisorManager.Application.Abstractions.Requests
{
    /// <summary>
    /// Represents the status of a request.
    /// </summary>
    public enum RequestStatus
    {
        Cancelled = -1,
        Completed = 0,
        Rejected = 1,
        Faulted = 2,
    }
}
