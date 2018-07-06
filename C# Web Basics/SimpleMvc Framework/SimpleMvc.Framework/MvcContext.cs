namespace SimpleMvc.Framework
{
    public class MvcContext
    {
        private static MvcContext instance;

        private MvcContext()
        {
        }

        #region Properties

        public static MvcContext Get => instance ?? (instance = new MvcContext());

        public string AssemblyName { get; set; }

        public string ControllersFolder { get; set; }

        public string ControllersSuffix { get; set; }

        public string ViewsFolder { get; set; }

        public string ModelsFolder { get; set; }

        public string ResourceFolder { get; set; }

        #endregion
    }
}