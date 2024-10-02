using System.Diagnostics;

namespace AdvisorManager.Application.Abstractions.Requests
{
    public abstract class RequestBase
    {
        public Guid RequestId { get; } = Guid.NewGuid();

        public long CreatedAt { get; } = Stopwatch.GetTimestamp();
    }
}
