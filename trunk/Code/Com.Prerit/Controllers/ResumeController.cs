using System.Web.Mvc;

using Com.Prerit.Filters;

namespace Com.Prerit.Controllers
{
    public partial class ResumeController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}