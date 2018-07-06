namespace WebServer.Server.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Contracts;
    using Enums;
    using HTTP.Contracts;
    using Routing.Contracts;

    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig serverRouteConfig)
        {
            this.serverRouteConfig = serverRouteConfig;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            foreach (KeyValuePair<string, IRoutingContext> kvp in this.serverRouteConfig.Routes[
                httpContext.Request.RequestMethod])
            {
                Regex regex = new Regex(kvp.Key);

                Match match = regex.Match(httpContext.Request.Path);

                if (!match.Success)
                {
                    continue;
                }

                foreach (string valueParameter in kvp.Value.Parameters)
                {
                    httpContext.Request.AddUrlParameter(valueParameter, match.Groups[valueParameter].Value);
                }

                return kvp.Value.RequestHandler.Handle(httpContext);
            }

            return null;
        }
    }
}