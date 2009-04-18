using System;
using System.Web.Mvc;
using System.Web.Routing;

using Com.Prerit.Web.Controllers;

namespace Com.Prerit.Web
{
    public class RegisterRoutesStartupTask : IStartupTask
    {
        #region Properties

        public RouteCollection Routes { get; private set; }

        #endregion

        #region Constructors

        public RegisterRoutesStartupTask()
            : this(RouteTable.Routes)
        {
        }

        public RegisterRoutesStartupTask(RouteCollection routes)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            Routes = routes;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            Routes.MapRoute("photo-albums",
                            PhotoAlbumsController.Name.Seo + "/{action}/{id}",
                            new
                                {
                                    controller = PhotoAlbumsController.Name.WithoutSuffix,
                                    action = PhotoAlbumsController.Action.Index,
                                    id = ""
                                });

            Routes.MapRoute("default",
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