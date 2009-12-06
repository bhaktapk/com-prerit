using System.Web.Mvc;
using System.Web.Routing;

using Com.Prerit.Controllers;

namespace Com.Prerit.Infrastructure.StartupTasks
{
    public class RegisterRoutesStartupTask : IStartupTask
    {
        #region Methods

        public void Execute()
        {
            RouteTable.Routes.IgnoreRoute("errors/{*pathInfo}");

            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RouteTable.Routes.IgnoreRoute("{controller}/{*pathInfo}", new { controller = DefaultMasterController.Name.WithoutSuffix });

            RouteTable.Routes.MapRoute("default",
                                       "{controller}/{action}/{id}",
                                       new
                                           {
                                               controller = AboutController.Name.WithoutSuffix,
                                               action = SharedAction.Index,
                                               id = ""
                                           });
        }

        public void Reset()
        {
            RouteTable.Routes.Clear();
        }

        #endregion
    }
}