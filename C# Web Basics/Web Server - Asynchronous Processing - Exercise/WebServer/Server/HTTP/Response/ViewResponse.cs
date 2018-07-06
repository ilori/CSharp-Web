namespace WebServer.Server.HTTP.Response
{
    using Enums;
    using Server.Contracts;

    public class ViewResponse : HttpResponse
    {
        public ViewResponse(HttpStatusCode statusCode, IView view) : base(statusCode, view)
        {
        }
    }
}