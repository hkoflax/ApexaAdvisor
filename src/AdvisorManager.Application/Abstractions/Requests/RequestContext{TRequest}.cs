using System.Diagnostics;

namespace AdvisorManager.Application.Abstractions.Requests
{
    public sealed class RequestContext<TRequest> : RequestContext
            where TRequest : RequestBase
    {
        public RequestContext(TRequest request, long completedTicks, RequestStatus status, Exception exception = null)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            CompletedAt = completedTicks;
            Elapsed = GetElapsedTime(request.CreatedAt, completedTicks);
            Status = status;
            Exception = exception;
        }

        private static TimeSpan GetElapsedTime(long createdAt, long completedTicks)
        {
            long elapsedTicks = completedTicks - createdAt;
            double elapsedMilliseconds = (double)elapsedTicks / Stopwatch.Frequency * 1000.0;
            return TimeSpan.FromMilliseconds(elapsedMilliseconds);
        }

        public TRequest Request { get; }
    }
}