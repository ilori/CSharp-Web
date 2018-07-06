namespace SimpleMvc.Framework.Controllers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Contracts;
    using Models;
    using Security;
    using ViewEngine;
    using Views;
    using WebServer.Http;
    using WebServer.Http.Contracts;

    public abstract class Controller
    {
        #region Constructor

        protected Controller()
        {
            this.ViewModel = new ViewModel();
            this.User = new Authentication();
        }

        #endregion

        #region Properties

        protected ViewModel ViewModel { get; }

        protected internal IHttpRequest Request { get; internal set; }

        protected internal Authentication User { get; private set; }

        protected string ErrorMessage { get; set; }

        #endregion

        #region Methods

        protected IViewable View([CallerMemberName] string caller = "")
        {
            string controllerName = this.GetType().Name.Replace(MvcContext.Get.ControllersSuffix, string.Empty);

            string fullName = $@"{MvcContext.Get.ViewsFolder}\{controllerName}\{caller}";

            IRenderable view = new View(fullName, this.ViewModel.Data);

            return new ViewResult(view);
        }

        protected IRedirectable RedirectToAction(string redirectUrl)
        {
            return new RedirectResult(redirectUrl);
        }

        protected bool IsValidModel(object bindingModel)
        {
            ValidationContext validationContext = new ValidationContext(bindingModel);

            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool validator = Validator.TryValidateObject(bindingModel, validationContext, validationResults, true);

            if (validationResults.Any())
            {
                this.ErrorMessage = validationResults.First().ErrorMessage;
                return false;
            }

            return true;

            //
            //TODO If custom validations are needed just comment the upper block and uncomment the block below

//            foreach (PropertyInfo property in bindingModel.GetType().GetProperties())
//            {
//                IEnumerable<Attribute> attributes =
//                    property.GetCustomAttributes().ToList();
//
//                if (!attributes.Any())
//                {
//                    continue;
//                }

//                foreach (PropertyAttribute attribute in attributes)
//                {
//                    if (!attribute.IsValid(property.GetValue(bindingModel)))
//                    {
//                        this.ErrorMessage = attribute.ErrorMessage;
//                        return false;
//                    }
//                }
//        }
        }

        protected internal virtual void InitializeController()
        {
            string user = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);

            if (user != null)
            {
                this.User = new Authentication(user);
            }
        }

        protected void SignIn(string name)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, name);
        }

        protected void SignOut()
        {
            this.Request.Session.Remove(SessionStore.CurrentUserKey);
        }

        public virtual void OnAuthentication()
        {
        }

        #endregion
    }
}