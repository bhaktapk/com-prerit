using System.Web.Mvc;

using Com.Prerit.Models.About;

namespace Com.Prerit.Controllers
{
    public partial class AboutController : Controller
    {
        #region Fields

        public static readonly string SeoName = ControllerUtil.GetSeoControllerName<AboutController>();

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