using System.Web.Mvc;

using Com.Prerit.Web.UI.Views.About;

namespace Com.Prerit.Web.UI.Controllers
{
    public class AboutController : DefaultMasterController<IndexModel>
    {
        #region Methods

        public ActionResult Index()
        {
            IndexModel model = CreateBaseModel();

            return View(model);
        }

        #endregion
    }
}