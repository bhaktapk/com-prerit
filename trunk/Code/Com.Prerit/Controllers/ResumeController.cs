using System.Web.Mvc;

using Com.Prerit.Models.Resume;

namespace Com.Prerit.Controllers
{
    public partial class ResumeController : Controller
    {
        #region Fields

        public static readonly string SeoName = ControllerUtil.GetSeoControllerName<ResumeController>();

        #endregion

        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [ActionName(ActionName.Index)]
        public virtual ActionResult Index()
        {
            var model = new IndexModel();

            return View(model);
        }

        #endregion

        #region Nested Type: ActionName

        public static class ActionName
        {
            #region Constants

            public const string Index = SharedAction.Index;

            #endregion
        }

        #endregion
    }
}