using System;
using System.Web.Mvc;
using System.Web.Routing;

using Com.Prerit.Infrastructure.Routing;
using Com.Prerit.Services;

namespace Com.Prerit.Infrastructure.StartupTasks
{
    public class RegisterRoutesStartupTask : IStartupTask
    {
        #region Constants

        private const string DefaultAction = "Index";

        #endregion

        #region Fields

        private readonly IAlbumService _albumService;

        #endregion

        #region Constructors

        public RegisterRoutesStartupTask(IAlbumService albumService)
        {
            if (albumService == null)
            {
                throw new ArgumentNullException("albumService");
            }

            _albumService = albumService;
        }

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
                                          new[]
                                              {
                                                  "controller", "action"
                                              },
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
                                          new[]
                                              {
                                                  "controller", "action"
                                              },
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

            RouteTable.Routes.MapSeoRoute("album routes with all albums",
                                          "{controller}",
                                          new[]
                                              {
                                                  "controller"
                                              },
                                          new
                                              {
                                                  action = MVC.Albums.Actions.AllAlbums
                                              },
                                          new
                                              {
                                                  controller = MVC.Albums.Name
                                              });

            RouteTable.Routes.MapSeoRoute("album routes by year",
                                          "{controller}/{year}",
                                          new[]
                                              {
                                                  "controller"
                                              },
                                          new
                                              {
                                                  action = MVC.Albums.Actions.AlbumsByYear,
                                              },
                                          new
                                              {
                                                  controller = MVC.Albums.Name,
                                                  year = new AlbumYearConstraint(_albumService)
                                              });

            RouteTable.Routes.MapSeoRoute("album routes by year and slug",
                                          "{controller}/{year}/{slug}",
                                          new[]
                                              {
                                                  "controller", "slug"
                                              },
                                          new
                                              {
                                                  action = MVC.Albums.Actions.AlbumsByYearAndSlug,
                                              },
                                          new
                                              {
                                                  controller = MVC.Albums.Name,
                                                  year = new AlbumYearConstraint(_albumService)
                                              });

            RouteTable.Routes.MapSeoRoute("resume format routes",
                                          "resume/formats/{action}",
                                          new[]
                                              {
                                                  "action"
                                              },
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