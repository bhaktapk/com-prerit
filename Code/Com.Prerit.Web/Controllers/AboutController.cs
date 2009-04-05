using System.Web.Mvc;

using Com.Prerit.Web.Models.About;

namespace Com.Prerit.Web.Controllers
{
    public class AboutController : DefaultMasterController
    {
        #region Methods

        [ActionName(Action.Index)]
        public ActionResult Index()
        {
            var model = CreateBaseModel<IndexModel>();

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

            public static readonly string Seo = GetSeoControllerName<AboutController>();

            public static readonly string WithoutSuffix = GetControllerNameWithoutSuffix<AboutController>();

            #endregion
        }

        #endregion
    }
}