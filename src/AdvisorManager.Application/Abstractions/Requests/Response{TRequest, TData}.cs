using Newtonsoft.Json;

namespace AdvisorManager.Application.Abstractions.Requests
{
    public class Response<TRequest, TData> : Response<TRequest>
        where TRequest : RequestBase<TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Response{TRequest, TData}"/> class.
        /// </summary>
        /// <param name="data">The query result.</param>
        /// <param name="context">The <see cref="RequestContext{TRequest}"/>.</param>
        internal Response(TData data, RequestContext<TRequest> context)
            : base(context)
        {
            Data = data;
        }

        /// <summary>
        /// Gets the data returned by the request.
        /// </summary>
        public TData Data { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            if (Data != null)
            {
                return $"RequestId-{Context.Request.RequestId} => {JsonConvert.SerializeObject(this, Formatting.Indented)}";
            }
            return $"RequestId-{Context.Request.RequestId}";
        }
    }
}