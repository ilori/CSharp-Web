namespace WebServer.Http.Response
{
    using System.Text;
    using Contracts;
    using Enums;

    public abstract class HttpResponse : IHttpResponse
    {
        protected HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
        }

        private string StatusCodeMessage => this.StatusCode.ToString();

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; }

        public HttpStatusCode StatusCode { get; protected set; }

        public override string ToString()
        {
            StringBuilder response = new StringBuilder();

            int statusCodeNumber = (int) this.StatusCode;
            response.AppendLine($"HTTP/1.1 {statusCodeNumber} {this.StatusCodeMessage}");

            response.AppendLine(this.Headers.ToString());

            return response.ToString();
        }
    }
}