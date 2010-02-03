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
        public AlbumsController() { }

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
        public System.Web.Mvc.ActionResult AlbumsByYearAndTitle() {
            return new T4MVC_ActionResult(Area, Name, Actions.AlbumsByYearAndTitle);
        }

        public readonly string Area = "";
        public readonly string Name = "Albums";

        static readonly ActionNames s_actions = new ActionNames();
        public ActionNames Actions { get { return s_actions; } }
        public class ActionNames {
            public readonly string AlbumsByYear = "AlbumsByYear";
            public readonly string AlbumsByYearAndTitle = "AlbumsByYearAndTitle";
            public readonly string Index = "Index";
        }


        static readonly ViewNames s_views = new ViewNames();
        public ViewNames Views { get { return s_views; } }
        public class ViewNames {
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

        public override System.Web.Mvc.ActionResult AlbumsByYearAndTitle(int year, string title) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.AlbumsByYearAndTitle);
            callInfo.RouteValues.Add("year", year);
            callInfo.RouteValues.Add("title", title);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Index() {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.Index);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
