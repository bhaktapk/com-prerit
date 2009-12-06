using System.Web;

using Com.Prerit.Infrastructure.StartupTasks;

namespace Com.Prerit.Infrastructure.HttpApplications
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