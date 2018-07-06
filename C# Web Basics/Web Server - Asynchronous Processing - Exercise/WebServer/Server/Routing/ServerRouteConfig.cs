namespace WebServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Contracts;
    using Enums;
    using Handlers.Contracts;

    public class ServerRouteConfig : IServerRouteConfig
    {
        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.Routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>>();

            foreach (HttpRequestMethod httpRequestMethod in Enum.GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>())
            {
                this.Routes.Add(httpRequestMethod, new Dictionary<string, IRoutingContext>());
            }

            this.InitializeServerConfig(appRouteConfig);
        }


        public IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes { get; }

        private void InitializeServerConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (KeyValuePair<HttpRequestMethod, IDictionary<string, IRequestHandler>> kvp in appRouteConfig.Routes)
            {
                foreach (KeyValuePair<string, IRequestHandler> requestHandler in kvp.Value)
                {
                    List<string> args = new List<string>();

                    string parsedRegex = this.ParseRoute(requestHandler.Key, args);

                    IRoutingContext context = new RoutingContext(args, requestHandler.Value);

                    this.Routes[kvp.Key].Add(parsedRegex, context);
                }
            }
        }

        private string ParseRoute(string requestHandlerKey, List<string> args)
        {
            StringBuilder parsedRegex = new StringBuilder();

            parsedRegex.Append('^');

            if (requestHandlerKey == "/")
            {
                parsedRegex.Append($"{requestHandlerKey}$");

                return parsedRegex.ToString();
            }

            string[] tokens = requestHandlerKey.Split('/');

            this.ParseTokens(args, tokens, parsedRegex);

            return parsedRegex.ToString();
        }

        private void ParseTokens(List<string> args, string[] tokens, StringBuilder parsedRegex)
        {
            for (int idx = 0; idx < tokens.Length; idx++)
            {
                string end = idx == tokens.Length - 1 ? "$" : "/";
                if (!tokens[idx].StartsWith("{") && !tokens[idx].EndsWith("}"))
                {
                    parsedRegex.Append($"{tokens[idx]}{end}");
                    continue;
                }

                string pattern = "<\\w+>";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(tokens[idx]);

                if (!match.Success)
                {
                    continue;
                }

                string paramName = match.Groups[0].Value.Substring(1, match.Groups[0].Length - 2);
                args.Add(paramName);
                parsedRegex.Append($"{tokens[idx].Substring(1, tokens[idx].Length - 2)}{end}");
            }
        }
    }
}