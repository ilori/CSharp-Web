namespace WebServer.Application.Views
{
    using Server.Contracts;

    public class HomeIndexView : IView
    {
        public string View()
        {
            return "<body><h1>WELCOME</h1></body>";
        }
    }
}