using System.Web;

namespace Com.Prerit.Web
{
    public class MvcApplication : HttpApplication
    {
        #region Methods

        protected void Application_Start()
        {
            StartupTaskRunner.Run();
        }

        #endregion
    }
}