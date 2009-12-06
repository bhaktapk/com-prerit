using System.Web;

using Com.Prerit.Web.Infrastructure.StartupTasks;

namespace Com.Prerit.Web.Infrastructure.HttpApplications
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