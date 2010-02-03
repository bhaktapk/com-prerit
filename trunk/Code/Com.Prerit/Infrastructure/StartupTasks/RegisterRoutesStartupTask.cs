using System.Web.Mvc;
using System.Web.Routing;

using Com.Prerit.Infrastructure.Routing;

namespace Com.Prerit.Infrastructure.StartupTasks
{
    public class RegisterRoutesStartupTask : IStartupTask
    {
        #region Constants

        private const string DefaultAction = "Index";

        #endregion

        #region Methods

        public void Execute()
        {
            RouteTable.Routes.IgnoreRoute("errors/{*pathInfo}");

            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // NOTE: lack of default controller forces ASP.NET MVC to generate full url instead of just "/"
            // NOTE: default controller is handled via IIS's Url Rewriting module
            RouteTable.Routes.MapSeoRoute("root routes with default action",
                                          "{controller}/{action}",
                                          new
                                              {
                                                  action = DefaultAction
                                              },
                                          new
                                              {
                                                  controller = new ListConstraint(new[]
                                                                                      {
                                                                                          MVC.About.Name, MVC.Contact.Name, MVC.OpenId.Name, MVC.Resume.Name
                                                                                      })
                                              });

            RouteTable.Routes.MapSeoRoute("account routes",
                                          "{controller}/{action}",
                                          null,
                                          new
                                              {
                                                  controller = MVC.Accounts.Name,
                                                  action = new ListConstraint(new[]
                                                                                  {
                                                                                      MVC.Accounts.Actions.LogIn, MVC.Accounts.Actions.LogOut,
                                                                                      MVC.Accounts.Actions.Unauthorized
                                                                                  })
                                              });

            RouteTable.Routes.MapSeoRoute("resume formats route",
                                          "resume/formats/{action}",
                                          new
                                              {
                                                  controller = MVC.ResumeFormats.Name
                                              });
        }

        public void Reset()
        {
            RouteTable.Routes.Clear();
        }

        #endregion
    }
}