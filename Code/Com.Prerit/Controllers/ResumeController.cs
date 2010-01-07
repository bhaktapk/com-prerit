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
        public virtual ActionResult Index()
        {
            var model = new IndexModel();

            return View(model);
        }

        #endregion
    }
}