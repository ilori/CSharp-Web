namespace SimpleMvc.Framework.Views
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Contracts;

    public class View : IRenderable
    {
        #region Constants

        private const string BaseLayoutFileName = "Layout";

        private const string ContentPlaceHolder = "{{{content}}}";

        private const string HtmlExtension = ".html";

        #endregion

        //VisualStudio Path
        //private const string LocalErrorPath = @"..\..\..\..\SimpleMvc.Framework\Errors\Error.html";
        //Rider Path

        #region PrivateFields

        private const string LocalErrorPath = @"..\SimpleMvc.Framework\Errors\Error.html";

        private readonly string templateFullName;

        private readonly IDictionary<string, string> viewData;

        #endregion

        #region Constructor

        public View(string templateFullName, IDictionary<string, string> viewData)
        {
            this.templateFullName = templateFullName;
            this.viewData = viewData;
        }

        #endregion

        #region Methods

        public string Render()
        {
            string fullHtml = this.ReadFile();

            if (this.viewData.Any())
            {
                foreach (KeyValuePair<string, string> parameter in this.viewData)
                {
                    fullHtml = fullHtml.Replace($"{{{{{{{parameter.Key}}}}}}}", parameter.Value);
                }
            }

            return fullHtml;
        }

        private string ReadFile()
        {
            string layoutHtml = this.RenderLayoutHtml();

            string templateFullNameWithExtension = this.templateFullName + HtmlExtension;

            // VisualStudio Path
            //string templateFullPath =
            //  $@"..\..\..\..\{MvcContext.Get.AssemblyName}\{templateFullNameWithExtension}";


            //Rider Path
            string templateFullPath =
                $@"{templateFullNameWithExtension}";

            if (!File.Exists(templateFullPath))
            {
                string errorHtml = File.ReadAllText(LocalErrorPath);
                this.viewData["error"] = "Requested view does not exist!";
                return errorHtml;
            }

            string templateContent = File.ReadAllText(templateFullPath);

            string result = layoutHtml.Replace(ContentPlaceHolder, templateContent);

            return result;
        }


        private string RenderLayoutHtml()
        {
            // VisualStudioPath
            //string layout =
            //  $@"..\..\..\..\{MvcContext.Get.AssemblyName}\{MvcContext.Get.ViewsFolder}\{BaseLayoutFileName}{HtmlExtension}";


            // RiderPath
            string layout =
                $@"{MvcContext.Get.ViewsFolder}\{BaseLayoutFileName}{HtmlExtension}";

            if (!File.Exists(layout))
            {
                string errorHtml = File.ReadAllText(LocalErrorPath);
                this.viewData["error"] = "Layout view does not exist!";
                return errorHtml;
            }

            string layoutContent = File.ReadAllText(layout);

            return layoutContent;
        }

        #endregion
    }
}