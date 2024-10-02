namespace AdvisorManager.Application.Abstractions.Requests
{
    public class RequestContext
    {
        public long CompletedAt { get; protected set; }

        public TimeSpan Elapsed { get; protected set; }

        public RequestStatus Status { get; protected set; }

        public Exception Exception { get; protected set; }
    }
}
