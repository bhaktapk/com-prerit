using System;
using System.Web.Mvc;
using System.Web.Security;

using Com.Prerit.Models.Accounts;
using Com.Prerit.Services;

using MvcContrib.Filters;

namespace Com.Prerit.Controllers
{
    public partial class AccountsController : Controller
    {
        #region Fields

        private readonly IMembershipService _membershipService;

        #endregion

        #region Constructors

        public AccountsController(IMembershipService membershipService)
        {
            if (membershipService == null)
            {
                throw new ArgumentNullException("membershipService");
            }

            _membershipService = membershipService;
        }

        #endregion

        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [ModelStateToTempData]
        public virtual ActionResult LogIn(string returnUrl)
        {
            string validatedReturnUrl = Uri.IsWellFormedUriString(returnUrl, UriKind.Relative) ? returnUrl : null;

            var model = new LogInModel { ReturnUrl = validatedReturnUrl };

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult LoginStatus()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new LoggedInStatusModel { Name = _membershipService.GetAccount(User.Identity.Name).Name };

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
        public virtual ActionResult LogOut(string returnUrl)
        {
            string validatedReturnUrl = Uri.IsWellFormedUriString(returnUrl, UriKind.Relative) ? returnUrl : null;

            FormsAuthentication.SignOut();

            if (!string.IsNullOrEmpty(validatedReturnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect(FormsAuthentication.DefaultUrl);
        }

        #endregion
    }
}