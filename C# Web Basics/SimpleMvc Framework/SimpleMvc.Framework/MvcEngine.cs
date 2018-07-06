namespace SimpleMvc.Framework
{
    using System;
    using System.Reflection;
    using WebServer;


    public static class MvcEngine
    {
        public static void Run(WebServer server)
        {
            #region InitializingMvcContext

            RegisterAssemblyName();
            RegisterControllerData();
            RegisterViewsData();
            RegisterModelsData();
            RegisterResourcesFolder();

            #endregion

            while (true)
            {
                try
                {
                    server.Run();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        #region Methods

        private static void RegisterResourcesFolder()
        {
            MvcContext.Get.ResourceFolder = "Content";
        }

        private static void RegisterModelsData()
        {
            MvcContext.Get.ModelsFolder = "Models";
        }

        private static void RegisterViewsData()
        {
            MvcContext.Get.ViewsFolder = "Views";
        }

        private static void RegisterControllerData()
        {
            MvcContext.Get.ControllersSuffix = "Controller";
            MvcContext.Get.ControllersFolder = "Controllers";
        }

        private static void RegisterAssemblyName()
        {
            MvcContext.Get.AssemblyName = Assembly.GetEntryAssembly().GetName().Name;
        }

        #endregion
    }
}