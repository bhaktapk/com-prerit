using System;
using System.Web.Mvc;

using Com.Prerit.Models.Accounts;
using Com.Prerit.Services;

using MvcContrib.Filters;

namespace Com.Prerit.Controllers
{
    public partial class AccountsController : Controller
    {
        #region Fields

        private readonly IFormsAuthenticationService _formsAuthenticationService;

        private readonly IMembershipService _membershipService;

        #endregion

        #region Constructors

        public AccountsController(IFormsAuthenticationService formsAuthenticationService, IMembershipService membershipService)
        {
            if (formsAuthenticationService == null)
            {
                throw new ArgumentNullException("formsAuthenticationService");
            }

            if (membershipService == null)
            {
                throw new ArgumentNullException("membershipService");
            }

            _formsAuthenticationService = formsAuthenticationService;
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
                return View(MVC.Accounts.Views.LoggingInStatus);
            }

            return View(MVC.Accounts.Views.NotLoggedInStatus);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult LogOut(string returnUrl)
        {
            string validatedReturnUrl = Uri.IsWellFormedUriString(returnUrl, UriKind.Relative) ? returnUrl : null;

            _formsAuthenticationService.SignOut();

            if (!string.IsNullOrEmpty(validatedReturnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect(_formsAuthenticationService.DefaultUrl);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Unauthorized(string returnUrl)
        {
            var model = new UnauthorizedModel { ReturnUrl = returnUrl };

            return View(model);
        }

        #endregion
    }
}