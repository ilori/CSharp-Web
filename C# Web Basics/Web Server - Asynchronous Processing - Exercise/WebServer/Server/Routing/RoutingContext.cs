namespace WebServer.Server.Routing
{
    using System.Collections.Generic;
    using Contracts;
    using Handlers.Contracts;

    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(IEnumerable<string> parameters, IRequestHandler requestHandler)
        {
            this.Parameters = parameters;
            this.RequestHandler = requestHandler;
        }

        public IEnumerable<string> Parameters { get; }
        public IRequestHandler RequestHandler { get; }
    }
}