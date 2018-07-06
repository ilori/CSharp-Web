namespace WebServer.Server.HTTP
{
    using Contracts;

    public class HttpContext : IHttpContext
    {
        public HttpContext(string requestString)
        {
            this.Request = new HttpRequest(requestString);
        }

        public IHttpRequest Request { get; }
    }
}