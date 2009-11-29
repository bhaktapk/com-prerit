using System.Web.Mvc;
using System.Web.Routing;

using Com.Prerit.Web.Controllers;

namespace Com.Prerit.Web.Infrastructure.StartupTasks
{
    public class RegisterRoutesStartupTask : IStartupTask
    {
        #region Methods

        public void Execute()
        {
            RouteTable.Routes.IgnoreRoute("errors/{*pathInfo}");

            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RouteTable.Routes.IgnoreRoute(PhotoAlbumsController.Name.WithoutSuffix);

            RouteTable.Routes.MapRoute("photo-albums",
                                       PhotoAlbumsController.Name.Seo + "/{action}/{id}",
                                       new
                                           {
                                               controller = PhotoAlbumsController.Name.WithoutSuffix,
                                               action = PhotoAlbumsController.Action.Index,
                                               id = ""
                                           });

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