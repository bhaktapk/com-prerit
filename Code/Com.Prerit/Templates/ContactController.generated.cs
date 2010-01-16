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
    public partial class ContactController {
        protected ContactController(Dummy d) { }

        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = (IT4MVCActionResult)result;
            return RedirectToRoute(callInfo.RouteValues);
        }

        [NonAction]
        public System.Web.Mvc.ActionResult SendEmail() {
            return new T4MVC_ActionResult(Area, Name, Actions.SendEmail);
        }

        public readonly string Area = "";
        public readonly string Name = "Contact";

        static readonly ActionNames s_actions = new ActionNames();
        public ActionNames Actions { get { return s_actions; } }
        public class ActionNames {
            public readonly string EmailSent = "EmailSent";
            public readonly string Index = "Index";
            public readonly string SendEmail = "SendEmail";
        }


        static readonly ViewNames s_views = new ViewNames();
        public ViewNames Views { get { return s_views; } }
        public class ViewNames {
            public readonly string EmailSent = "~/Views/Contact/EmailSent.aspx";
            public readonly string Index = "~/Views/Contact/Index.aspx";
            public readonly string ValidationSummary = "~/Views/Contact/ValidationSummary.ascx";
        }
    }

    [CompilerGenerated]
    public class T4MVC_ContactController: Com.Prerit.Controllers.ContactController {
        public T4MVC_ContactController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult EmailSent() {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.EmailSent);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Index() {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.Index);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SendEmail(Com.Prerit.Models.Contact.IndexModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.SendEmail);
            callInfo.RouteValues.Add("model", model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591