using System.Web.Mvc;
using System.Web.Security;

using Com.Prerit.Models.Accounts;

using MvcContrib.Filters;

namespace Com.Prerit.Controllers
{
    public partial class AccountsController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [ModelStateToTempData]
        public virtual ActionResult Login(string returnUrl)
        {
            var model = new LoginModel { ReturnUrl = returnUrl };

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult LoginStatus()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new LoggedInStatusModel();

                return View(MVC.Accounts.Views.LoggedInStatus, model);
            }

            if (ControllerUtil.IsLoginRequest(Request.Url, Url))
            {
                var model = new LoggingInStatusModel();

                return View(MVC.Accounts.Views.LoggingInStatus, model);
            }
            else
            {
                var model = new NotLoggedInStatusModel();

                return View(MVC.Accounts.Views.NotLoggedInStatus, model);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Logout(string returnUrl)
        {
            FormsAuthentication.SignOut();

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect(FormsAuthentication.DefaultUrl);
        }

        #endregion
    }
}