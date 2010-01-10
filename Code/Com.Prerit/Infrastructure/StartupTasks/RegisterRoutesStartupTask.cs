using System.Web.Mvc;
using System.Web.Routing;

using Com.Prerit.Controllers;
using Com.Prerit.Infrastructure.Routing;

namespace Com.Prerit.Infrastructure.StartupTasks
{
    public class RegisterRoutesStartupTask : IStartupTask
    {
        #region Methods

        public void Execute()
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RouteTable.Routes.IgnoreRoute("{controller}/{*pathInfo}", new { controller = MVC.DefaultMaster.Name });

            RouteTable.Routes.MapSeoRoute("resume formats", "resume/formats/{action}", new { controller = MVC.ResumeFormats.Name });

            // NOTE: lack of default controller forces ASP.NET MVC to generate full url instead of just "/"
            // NOTE: default controller is handled via IIS's Url Rewriting module
            RouteTable.Routes.MapSeoRoute("default", "{controller}/{action}", new { action = SharedAction.Index });
        }

        public void Reset()
        {
            RouteTable.Routes.Clear();
        }

        #endregion
    }
}