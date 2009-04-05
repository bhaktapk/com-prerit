using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Com.Prerit.Web.Controllers;

namespace Com.Prerit.Web
{
    public class MvcApplication : HttpApplication
    {
        #region Methods

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("photo-albums",
                            PhotoAlbumsController.Name.Seo + "/{action}/{id}",
                            new
                                {
                                    controller = PhotoAlbumsController.Name.WithoutSuffix,
                                    action = PhotoAlbumsController.Action.Index,
                                    id = ""
                                });

            routes.MapRoute("default",
                            "{controller}/{action}/{id}",
                            new
                                {
                                    controller = AboutController.Name.WithoutSuffix,
                                    action = SharedAction.Index,
                                    id = ""
                                });
        }

        #endregion
    }
}