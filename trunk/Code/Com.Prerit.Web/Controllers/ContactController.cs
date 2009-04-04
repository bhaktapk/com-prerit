using System.Web.Mvc;

using Com.Prerit.Web.Models.Contact;

namespace Com.Prerit.Web.Controllers
{
    public class ContactController : DefaultMasterController
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