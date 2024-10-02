using Newtonsoft.Json;

namespace AdvisorManager.Application.Abstractions.Requests
{
    public class Response<TRequest>
            where TRequest : RequestBase
    {
        internal Response(RequestContext<TRequest> context)
        {
            Context = context;
        }

        public bool Succeeded => Context?.Status == RequestStatus.Completed;

        public RequestContext<TRequest> Context { get; }

        public override string ToString()
        {
            return $"RequestId-{Context.Request.RequestId} => {JsonConvert.SerializeObject(this, Formatting.Indented)}";
        }
    }
}