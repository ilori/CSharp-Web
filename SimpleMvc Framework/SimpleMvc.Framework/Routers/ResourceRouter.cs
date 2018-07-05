namespace SimpleMvc.Framework.Routers
{
    using System;
    using System.IO;
    using System.Linq;
    using WebServer.Contracts;
    using WebServer.Enums;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;

    public class ResourceRouter : IHandleable
    {
        public IHttpResponse Handle(IHttpRequest request)
        {
            string fileFullName = request.Path.Split("/").Last();
            string fileExtension = request.Path.Split(".").Last();

            IHttpResponse response = null;

            try
            {
                byte[] fileContent = this.ReadFileContent(fileFullName, fileExtension);

                response = new FileResponse(HttpStatusCode.Found, fileContent);
            }
            catch (Exception)
            {
                return new NotFoundResponse();
            }

            return response;
        }

        private byte[] ReadFileContent(string fileFullName, string fileExtension)
        {
            //Visual Studio Path
            // string path =
            //  $@"..\..\..\..\{MvcContext.Get.AssemblyName}\{MvcContext.Get.ResourceFolder}\{fileExtension}\{fileFullName}";

            //Rider Path

            string path = $@"{MvcContext.Get.ResourceFolder}\{fileExtension}\{fileFullName}";

            byte[] byteContent = File.ReadAllBytes(path);

            return byteContent;
        }
    }
}