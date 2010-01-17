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
    public partial class OpenIdController {
        public OpenIdController() { }

        protected OpenIdController(Dummy d) { }

        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = (IT4MVCActionResult)result;
            return RedirectToRoute(callInfo.RouteValues);
        }

        [NonAction]
        public System.Web.Mvc.ActionResult CreateRequest() {
            return new T4MVC_ActionResult(Area, Name, Actions.CreateRequest);
        }

        public readonly string Area = "";
        public readonly string Name = "OpenId";

        static readonly ActionNames s_actions = new ActionNames();
        public ActionNames Actions { get { return s_actions; } }
        public class ActionNames {
            public readonly string CreateRequest = "CreateRequest";
            public readonly string Xrds = "Xrds";
        }


        static readonly ViewNames s_views = new ViewNames();
        public ViewNames Views { get { return s_views; } }
        public class ViewNames {
            public readonly string Xrds = "~/Views/OpenId/Xrds.aspx";
        }
    }

    [CompilerGenerated]
    public class T4MVC_OpenIdController: Com.Prerit.Controllers.OpenIdController {
        public T4MVC_OpenIdController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult CreateRequest(string returnUrl) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.CreateRequest);
            callInfo.RouteValues.Add("returnUrl", returnUrl);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Xrds() {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.Xrds);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
