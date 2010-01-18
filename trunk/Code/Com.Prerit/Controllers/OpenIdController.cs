using System.Web.Mvc;

using Com.Prerit.Filters;
using Com.Prerit.Models.OpenId;

using MvcContrib.Filters;

namespace Com.Prerit.Controllers
{
    public partial class OpenIdController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Post)]
        [ModelToTempData]
        [ModelStateToTempData]
        public virtual ActionResult CreateRequest(CreateRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(MVC.Accounts.Login(model.ReturnUrl));
            }

            // TODO: create request

            return !string.IsNullOrEmpty(model.ReturnUrl) ? (ActionResult) Redirect(model.ReturnUrl) : RedirectToAction(MVC.About.Index());
        }

        public virtual ActionResult Xrds()
        {
            var model = new XrdsModel();

            return View(model);
        }

        #endregion
    }
}