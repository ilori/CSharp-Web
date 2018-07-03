namespace WebServer.Server.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Contracts;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            this.headers[header.Key] = header;
        }

        public bool ContainsKey(string key)
        {
            return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            return this.ContainsKey(key) ? this.headers[key] : null;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (var header in this.headers)
            {
                result.AppendLine(header.Value.ToString());
            }

            return result.ToString();
        }
    }
}