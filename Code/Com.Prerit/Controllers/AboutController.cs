using System.Web.Mvc;

using Com.Prerit.Models.About;

namespace Com.Prerit.Controllers
{
    public class AboutController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [ActionName(Action.Index)]
        public ActionResult Index()
        {
            var model = new IndexModel();

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

            public static readonly string Seo = ControllerUtil.GetSeoControllerName<AboutController>();

            public static readonly string WithoutSuffix = ControllerUtil.GetControllerNameWithoutSuffix<AboutController>();

            #endregion
        }

        #endregion
    }
}