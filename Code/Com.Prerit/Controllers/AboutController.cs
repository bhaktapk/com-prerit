using System.Web.Mvc;

using Com.Prerit.Filters;
using Com.Prerit.Models.About;

namespace Com.Prerit.Controllers
{
    public partial class AboutController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [XrdsLocation]
        public virtual ActionResult Index()
        {
            var model = new IndexModel();

            return View(model);
        }

        #endregion
    }
}