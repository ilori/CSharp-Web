namespace WebServer.Server.HTTP.Response
{
    using System.Text;
    using Contracts;
    using Enums;
    using Server.Contracts;

    public abstract class HttpResponse : IHttpResponse
    {
        private readonly IView view;

        protected HttpResponse(string redirectUrl)
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.StatusCode = HttpStatusCode.Found;
            this.AddHeader("Location", redirectUrl);
        }

        protected HttpResponse(HttpStatusCode statusCode, IView view)
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.StatusCode = statusCode;
            this.view = view;
        }

        private IHttpHeaderCollection HeaderCollection { get; set; }

        private HttpStatusCode StatusCode { get; set; }

        private string StatusMessage => this.StatusCode.ToString();

        public string Response
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"HTTP/1.1 {(int) this.StatusCode} {this.StatusMessage}")
                    .AppendLine(this.HeaderCollection.ToString()).AppendLine();

                if ((int) this.StatusCode < 300 || (int) this.StatusCode > 400)
                {
                    sb.AppendLine(this.view.View());
                }

                return sb.ToString();
            }
        }

        public void AddHeader(string key, string value)
        {
            this.HeaderCollection.Add(new HttpHeader(key, value));
        }
    }
}