using System.Web.Mvc;

using Com.Prerit.Web.UI.Views.About;

namespace Com.Prerit.Web.UI.Controllers
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