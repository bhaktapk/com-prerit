using System.Web.Mvc;

using Com.Prerit.Models.OpenId;

namespace Com.Prerit.Controllers
{
    public partial class OpenIdController : Controller
    {
        #region Methods

        public virtual ActionResult Xrds()
        {
            var model = new XrdsModel();

            return View(model);
        }

        #endregion
    }
}