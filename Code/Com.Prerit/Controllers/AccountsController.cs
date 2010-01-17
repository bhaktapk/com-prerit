using System.Web.Mvc;
using System.Web.Security;

using Com.Prerit.Models.Accounts;

namespace Com.Prerit.Controllers
{
    public partial class AccountsController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
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
                var model = new LoggedInModel();

                return View(MVC.Accounts.Views.LoggedIn, model);
            }

            if (ControllerUtil.IsLoginRequest(Request.Url, Url))
            {
                var model = new LoggingInModel();

                return View(MVC.Accounts.Views.LoggingIn, model);
            }
            else
            {
                var model = new NotLoggedInModel();

                return View(MVC.Accounts.Views.NotLoggedIn, model);
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