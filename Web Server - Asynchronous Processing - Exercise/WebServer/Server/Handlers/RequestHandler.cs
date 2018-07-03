namespace WebServer.Server.Handlers
{
    using System;
    using Contracts;
    using HTTP.Contracts;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpContext, IHttpResponse> func;

        protected RequestHandler(Func<IHttpContext, IHttpResponse> func)
        {
            this.func = func;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse response = this.func.Invoke(httpContext);

            response.AddHeader("Content-Type", "text/html");

            return response;
        }
    }
}