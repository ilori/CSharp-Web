namespace WebServer.Server.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Contracts;
    using Enums;
    using Exceptions;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestStr)
        {
            this.FormData = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.UrlParameters = new Dictionary<string, string>();
            this.HeaderCollection = new HttpHeaderCollection();

            this.ParseRequest(requestStr);
        }

        public IDictionary<string, string> FormData { get; }
        public IDictionary<string, string> QueryParameters { get; }
        public IDictionary<string, string> UrlParameters { get; }
        public IHttpHeaderCollection HeaderCollection { get; }
        public HttpRequestMethod RequestMethod { get; private set; }
        public string Path { get; private set; }
        public string Url { get; private set; }


        private void ParseRequest(string requestStr)
        {
            string[] requestLines = requestStr.Split(Environment.NewLine);

            string[] requestLine = requestLines[0].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                throw new BadRequestException("Invalid request line");
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine[0]);
            this.Url = requestLine[1];
            this.Path = this.Url.Split(new[] {"?", "#"}, StringSplitOptions.RemoveEmptyEntries)[0];

            this.ParseHeaders(requestLines);
            this.ParseParameters();

            if (this.RequestMethod == HttpRequestMethod.Post)
            {
                this.ParseQuery(requestLines[requestLines.Length - 1], this.FormData);
            }
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains("?"))
            {
                return;
            }

            string query = this.Url.Split("?")[1];

            this.ParseQuery(query, this.QueryParameters);
        }

        private void ParseQuery(string query, IDictionary<string, string> queryParameters)
        {
            if (!query.Contains("="))
            {
                return;
            }

            string[] queryPairs = query.Split("&");

            foreach (string queryPair in queryPairs)
            {
                string[] queryArgs = queryPair.Split("=");

                if (queryArgs.Length != 2)
                {
                    continue;
                }

                string key = WebUtility.UrlDecode(queryArgs[0]);
                string value = WebUtility.UrlDecode(queryArgs[1]);

                queryParameters.Add(key, value);
            }
        }

        private void ParseHeaders(string[] requestLines)
        {
            int endIndex = Array.IndexOf(requestLines, string.Empty);

            for (int i = 1; i < endIndex; i++)
            {
                string[] headerArgs = requestLines[i].Split(":", StringSplitOptions.RemoveEmptyEntries);

                string key = headerArgs[0];
                string value = headerArgs[1].Trim();

                HttpHeader header = new HttpHeader(key, value);

                this.HeaderCollection.Add(header);
            }

            if (!this.HeaderCollection.ContainsKey("Host"))
            {
                throw new BadRequestException("Invalid header!");
            }
        }

        private HttpRequestMethod ParseRequestMethod(string method)
        {
            if (!Enum.TryParse(method, true, out HttpRequestMethod result))
            {
                throw new BadRequestException("Invalid request method");
            }

            return result;
        }

        public void AddUrlParameter(string key, string value)
        {
            this.UrlParameters[key] = value;
        }
    }
}