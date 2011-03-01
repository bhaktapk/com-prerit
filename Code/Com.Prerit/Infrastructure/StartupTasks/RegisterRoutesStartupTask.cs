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

        private readonly RouteCollection _routes;

        #endregion

        #region Constructors

        public RegisterRoutesStartupTask(RouteCollection routes, IAlbumService albumService)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            if (albumService == null)
            {
                throw new ArgumentNullException("albumService");
            }

            _routes = routes;
            _albumService = albumService;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            _routes.IgnoreRoute("errors/{*pathInfo}");

            _routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // NOTE: lack of default controller forces ASP.NET MVC to generate full url instead of just "/"
            // NOTE: default controller is handled via IIS's Url Rewriting module
            _routes.MapSeoRoute("root routes with default action",
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

            _routes.MapSeoRoute("account routes",
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

            _routes.MapSeoRoute("album routes with all albums",
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

            _routes.MapSeoRoute("album routes by year",
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

            _routes.MapSeoRoute("album routes by year and slug",
                                          "{controller}/{year}/{slug}",
                                          new[]
                                              {
                                                  "controller"
                                              },
                                          new
                                              {
                                                  action = MVC.Albums.Actions.AlbumByYearAndSlug,
                                              },
                                          new
                                              {
                                                  controller = MVC.Albums.Name,
                                                  slug = new AlbumSlugConstraint("year", _albumService)
                                              });

            _routes.MapSeoRoute("album photo routes by year and slug",
                                          "{controller}/{year}/{slug}/{action}",
                                          new[]
                                              {
                                                  "controller", "action"
                                              },
                                          null,
                                          new
                                              {
                                                  controller = MVC.Albums.Name,
                                                  slug = new AlbumSlugConstraint("year", _albumService),
                                                  action = MVC.Albums.Actions.Portrait
                                              });

            _routes.MapSeoRoute("album photo routes by year, slug and photo item",
                                          "{controller}/{year}/{slug}/photos/{photoItem}/types/{action}",
                                          new[]
                                              {
                                                  "controller", "action"
                                              },
                                          null,
                                          new
                                              {
                                                  controller = MVC.Albums.Name,
                                                  photoItem = new AlbumPhotoItemConstraint("year", "slug", _albumService),
                                                  action = new ListConstraint(new[]
                                                                                  {
                                                                                      MVC.Albums.Actions.Thumbnail, MVC.Albums.Actions.WebOptimized
                                                                                  })
                                              });

            _routes.MapSeoRoute("resume format routes",
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
            _routes.Clear();
        }

        #endregion
    }
}