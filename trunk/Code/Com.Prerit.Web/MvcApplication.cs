using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
                            "photo-albums/{action}/{id}",
                            new
                                {
                                    controller = "photoalbums",
                                    action = "index",
                                    id = ""
                                });

            routes.MapRoute("default",
                            "{controller}/{action}/{id}",
                            new
                                {
                                    controller = "about",
                                    action = "index",
                                    id = ""
                                });
        }

        #endregion
    }
}