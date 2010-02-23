// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Com.Prerit.Controllers {
    [CompilerGenerated]
    public partial class AlbumsController {
        protected AlbumsController(Dummy d) { }

        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = (IT4MVCActionResult)result;
            return RedirectToRoute(callInfo.RouteValues);
        }

        [NonAction]
        public System.Web.Mvc.ActionResult AlbumsByYear() {
            return new T4MVC_ActionResult(Area, Name, Actions.AlbumsByYear);
        }
        [NonAction]
        public System.Web.Mvc.ActionResult AlbumsByYearAndSlug() {
            return new T4MVC_ActionResult(Area, Name, Actions.AlbumsByYearAndSlug);
        }
        [NonAction]
        public System.Web.Mvc.ActionResult Portrait() {
            return new T4MVC_ActionResult(Area, Name, Actions.Portrait);
        }
        [NonAction]
        public System.Web.Mvc.ActionResult Thumbnail() {
            return new T4MVC_ActionResult(Area, Name, Actions.Thumbnail);
        }
        [NonAction]
        public System.Web.Mvc.ActionResult WebOptimized() {
            return new T4MVC_ActionResult(Area, Name, Actions.WebOptimized);
        }

        public readonly string Area = "";
        public readonly string Name = "Albums";

        static readonly ActionNames s_actions = new ActionNames();
        public ActionNames Actions { get { return s_actions; } }
        public class ActionNames {
            public readonly string AlbumsByYear = "AlbumsByYear";
            public readonly string AlbumsByYearAndSlug = "AlbumsByYearAndSlug";
            public readonly string AllAlbums = "AllAlbums";
            public readonly string Portrait = "Portrait";
            public readonly string Thumbnail = "Thumbnail";
            public readonly string WebOptimized = "WebOptimized";
        }


        static readonly ViewNames s_views = new ViewNames();
        public ViewNames Views { get { return s_views; } }
        public class ViewNames {
            public readonly string AlbumPortrait = "~/Views/Albums/AlbumPortrait.ascx";
            public readonly string AlbumsByYear = "~/Views/Albums/AlbumsByYear.ascx";
            public readonly string AllAlbums = "~/Views/Albums/AllAlbums.aspx";
            public readonly string NoAlbums = "~/Views/Albums/NoAlbums.aspx";
        }
    }

    [CompilerGenerated]
    public class T4MVC_AlbumsController: Com.Prerit.Controllers.AlbumsController {
        public T4MVC_AlbumsController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult AlbumsByYear(int year) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.AlbumsByYear);
            callInfo.RouteValues.Add("year", year);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult AlbumsByYearAndSlug(int year, string slug) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.AlbumsByYearAndSlug);
            callInfo.RouteValues.Add("year", year);
            callInfo.RouteValues.Add("slug", slug);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult AllAlbums() {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.AllAlbums);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Portrait(int year, string slug) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.Portrait);
            callInfo.RouteValues.Add("year", year);
            callInfo.RouteValues.Add("slug", slug);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Thumbnail(int year, string slug, int photoIndex) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.Thumbnail);
            callInfo.RouteValues.Add("year", year);
            callInfo.RouteValues.Add("slug", slug);
            callInfo.RouteValues.Add("photoIndex", photoIndex);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult WebOptimized(int year, string slug, int photoIndex) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.WebOptimized);
            callInfo.RouteValues.Add("year", year);
            callInfo.RouteValues.Add("slug", slug);
            callInfo.RouteValues.Add("photoIndex", photoIndex);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
