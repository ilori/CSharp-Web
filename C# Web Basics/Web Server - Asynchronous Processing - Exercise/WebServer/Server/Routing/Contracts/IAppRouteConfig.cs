namespace WebServer.Server.Routing.Contracts
{
    using System.Collections.Generic;
    using Enums;
    using Handlers.Contracts;

    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, IRequestHandler>> Routes { get; }

        void AddRoute(string route, IRequestHandler handler);
    }
}