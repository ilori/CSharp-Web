namespace WebServer.Server.HTTP.Contracts
{
    using System.Collections.Generic;
    using Enums;

    public interface IHttpRequest
    {
        IDictionary<string, string> FormData { get; }

        IDictionary<string, string> QueryParameters { get; }

        IDictionary<string, string> UrlParameters { get; }

        IHttpHeaderCollection HeaderCollection { get; }

        HttpRequestMethod RequestMethod { get; }

        string Path { get; }

        string Url { get; }

        void AddUrlParameter(string key, string value);
    }
}