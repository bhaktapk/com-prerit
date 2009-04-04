using System.Web.Mvc;

using Com.Prerit.Web.Models.About;

namespace Com.Prerit.Web.Controllers
{
    public class AboutController : DefaultMasterController
    {
        #region Methods

        public ActionResult Index()
        {
            var model = CreateBaseModel<IndexModel>();

            return View(model);
        }

        #endregion
    }
}