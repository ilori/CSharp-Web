namespace SimpleMvc.Framework.Routers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Attributes.Methods;
    using Contracts;
    using Controllers;
    using WebServer.Contracts;
    using WebServer.Enums;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;

    public class ControllerRouter : IHandleable
    {
        private string controllerName;
        private string actionName;

        public IHttpResponse Handle(IHttpRequest request)
        {
            IDictionary<string, string> getParams = request.UrlParameters;

            IDictionary<string, string> postParams = request.FormData;

            string requestMethod = request.Method.ToString();

            string[] controllerParams = request.Path.Split("/", StringSplitOptions.RemoveEmptyEntries);

            if (controllerParams.Length != 2)
            {
                if (controllerParams.Length == 0)
                {
                    this.controllerName = $"Home{MvcContext.Get.ControllersSuffix}";
                    this.actionName = "Index";
                }
                else
                {
                    return new NotFoundResponse();
                }
            }
            else
            {
                this.controllerName = controllerParams[0][0].ToString().ToUpper() +
                                      controllerParams[0].Substring(1) + MvcContext.Get.ControllersSuffix;

                this.actionName = controllerParams[1][0].ToString().ToUpper() + controllerParams[1].Substring(1);
            }

            Controller controller = this.GetController(controllerName);


            if (controller == null)
            {
                return new NotFoundResponse();
            }

            controller.Request = request;
            controller.InitializeController();
            controller.OnAuthentication();

            MethodInfo method = this.GetMethod(controller, requestMethod, actionName);

            if (method == null)
            {
                return new NotFoundResponse();
            }

            IEnumerable<ParameterInfo> parameters = method.GetParameters();

            object[] methodParams = this.AddParameters(parameters, getParams, postParams);

            try
            {
                IHttpResponse response = this.GetResponse(method, controller, methodParams);

                return response;
            }
            catch (Exception e)
            {
                return new InternalServerErrorResponse(e);
            }
        }

        private IHttpResponse GetResponse(MethodInfo method, Controller controller, object[] methodParams)
        {
            object actionResult = method.Invoke(controller, methodParams);

            switch (actionResult)
            {
                case IViewable viewable:

                    string content = viewable.Invoke();

                    return new ContentResponse(HttpStatusCode.Ok, content);

                case IRedirectable redirectable:

                    string redirectUrl = redirectable.Invoke();

                    return new RedirectResponse(redirectUrl);

                default:
                    return null;
            }
        }

        private object[] AddParameters(IEnumerable<ParameterInfo> parameters, IDictionary<string, string> getParams,
            IDictionary<string, string> postParams)
        {
            object[] methodParams = new object[parameters.Count()];

            int index = 0;

            foreach (ParameterInfo parameter in parameters)
            {
                if (parameter.ParameterType.IsPrimitive || parameter.ParameterType == typeof(string))
                {
                    methodParams[index] = this.ProcessPrimitiveParameter(parameter, getParams);
                    index++;
                }
                else
                {
                    methodParams[index] = this.ProcessComplexParameter(parameter, postParams);
                    index++;
                }
            }

            return methodParams;
        }

        private object ProcessComplexParameter(ParameterInfo parameter, IDictionary<string, string> postParams)
        {
            Type bindingModelType = parameter.ParameterType;
            object bindingModel = Activator.CreateInstance(bindingModelType);

            IEnumerable<PropertyInfo> properties = bindingModelType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                property.SetValue(bindingModel, Convert.ChangeType(postParams[property.Name], property.PropertyType));
            }

            return Convert.ChangeType(bindingModel, bindingModelType);
        }

        private object ProcessPrimitiveParameter(ParameterInfo parameter, IDictionary<string, string> getParams)
        {
            object value = getParams[parameter.Name];
            return Convert.ChangeType(value, parameter.ParameterType);
        }

        private MethodInfo GetMethod(Controller controller, string requestMethod, string action)
        {
            foreach (MethodInfo method in controller.GetType().GetMethods().Where(x => x.Name == action))
            {
                IEnumerable<HttpMethodAttribute> httpMethodAttributes = method
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>();

                if (!httpMethodAttributes.Any() && requestMethod.ToUpper() == "GET")
                {
                    return method;
                }

                foreach (HttpMethodAttribute httpMethodAttribute in httpMethodAttributes)
                {
                    if (httpMethodAttribute.IsValid(requestMethod))
                    {
                        return method;
                    }
                }
            }

            return null;
        }


        private Controller GetController(string controllerType)
        {
            string controllerFullName =
                $"{MvcContext.Get.AssemblyName}.{MvcContext.Get.ControllersFolder}.{controllerType}";

            Type type = Assembly.GetEntryAssembly().GetTypes().SingleOrDefault(x => x.FullName == controllerFullName);

            if (type == null)
            {
                return null;
            }

            Controller controller = (Controller) Activator.CreateInstance(type);

            return controller;
        }
    }
}