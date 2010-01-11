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
            RouteTable.Routes.IgnoreRoute("errors/{*pathInfo}");

            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // NOTE: lack of default controller forces ASP.NET MVC to generate full url instead of just "/"
            // NOTE: default controller is handled via IIS's Url Rewriting module
            RouteTable.Routes.MapSeoRoute("root routes",
                                          "{controller}/{action}",
                                          new { action = SharedAction.Index },
                                          new { controller = new ListConstraint(MVC.About.Name, MVC.Contact.Name, MVC.Resume.Name) });

            RouteTable.Routes.MapSeoRoute("resume formats route", "resume/formats/{action}", new { controller = MVC.ResumeFormats.Name });
        }

        public void Reset()
        {
            RouteTable.Routes.Clear();
        }

        #endregion
    }
}