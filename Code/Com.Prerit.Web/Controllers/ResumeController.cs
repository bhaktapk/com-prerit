using System.Web.Mvc;

using Com.Prerit.Web.Models.Resume;

namespace Com.Prerit.Web.Controllers
{
    public class ResumeController : DefaultMasterController
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [ActionName(Action.Index)]
        public ActionResult Index()
        {
            IndexModel model = UpdateModelBase(new IndexModel());

            return View(model);
        }

        #endregion

        #region Nested Type: Action

        public static class Action
        {
            #region Constants

            public const string Index = SharedAction.Index;

            #endregion
        }

        #endregion

        #region Nested Type: Name

        public static class Name
        {
            #region Fields

            public static readonly string Seo = GetSeoControllerName<ResumeController>();

            public static readonly string WithoutSuffix = GetControllerNameWithoutSuffix<ResumeController>();

            #endregion
        }

        #endregion
    }
}