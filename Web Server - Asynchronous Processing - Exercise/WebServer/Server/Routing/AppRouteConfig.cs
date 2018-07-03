namespace WebServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Enums;
    using Handlers.Contracts;

    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly IDictionary<HttpRequestMethod, IDictionary<string, IRequestHandler>> routes;

        public AppRouteConfig()
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRequestHandler>>();

            foreach (HttpRequestMethod httpRequestMethod in Enum.GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>())
            {
                this.routes.Add(httpRequestMethod, new Dictionary<string, IRequestHandler>());
            }
        }

        public IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, IRequestHandler>> Routes =>
            (IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, IRequestHandler>>) this.routes;

        public void AddRoute(string route, IRequestHandler handler)
        {
            if (handler.GetType().ToString().ToLower().Contains("get"))
            {
                this.routes[HttpRequestMethod.Get].Add(route, handler);
            }
            else if (handler.GetType().ToString().ToLower().Contains("post"))
            {
                this.routes[HttpRequestMethod.Post].Add(route, handler);
            }
        }
    }
}