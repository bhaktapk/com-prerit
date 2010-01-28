using System;
using System.Web.Mvc;

using Com.Prerit.Filters;
using Com.Prerit.Models.OpenId;
using Com.Prerit.Services;

using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.RelyingParty;

using MvcContrib.Filters;

namespace Com.Prerit.Controllers
{
    public partial class OpenIdController : Controller
    {
        #region Fields

        private readonly IFormsAuthenticationService _formsAuthenticationService;

        private readonly IOpenIdService _openIdService;

        #endregion

        #region Constructors

        public OpenIdController(IFormsAuthenticationService formsAuthenticationService, IOpenIdService openIdService)
        {
            if (formsAuthenticationService == null)
            {
                throw new ArgumentNullException("formsAuthenticationService");
            }

            if (openIdService == null)
            {
                throw new ArgumentNullException("openIdService");
            }

            _formsAuthenticationService = formsAuthenticationService;
            _openIdService = openIdService;
        }

        #endregion

        #region Methods

        [AcceptVerbs(HttpVerbs.Get)]
        [XrdsLocation]
        public virtual ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("Request")]
        public virtual ActionResult RequestAuth(string returnUrl)
        {
            string validatedReturnUrl = Uri.IsWellFormedUriString(returnUrl, UriKind.Relative) ? returnUrl : null;

            string realmUrl = Url.Action(MVC.OpenId.Index());

            string respondUrl = Url.Action(MVC.OpenId.Respond(validatedReturnUrl));

            IAuthenticationRequest request = _openIdService.CreateRequest(realmUrl, respondUrl);

            return request.RedirectingResponse.AsActionResult();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ModelStateToTempData]
        public virtual ActionResult Respond(string returnUrl)
        {
            string validatedReturnUrl = Uri.IsWellFormedUriString(returnUrl, UriKind.Relative) ? returnUrl : null;

            IAuthenticationResponse response = _openIdService.GetResponse();

            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        _formsAuthenticationService.SetAuthCookie(response.ClaimedIdentifier, false);

                        if (!string.IsNullOrEmpty(validatedReturnUrl))
                        {
                            return Redirect(validatedReturnUrl);
                        }

                        return RedirectToAction(MVC.About.Index());
                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("Canceled", "The authentication was canceled.");
                        break;
                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError(response.Exception.Message, response.Exception.Message);
                        break;
                    case AuthenticationStatus.ExtensionsOnly:
                        ModelState.AddModelError("ExtensionsOnly",
                                                 "Google sent a message that did not contain an identity assertion, but may carry OpenID extensions.");
                        break;
                    case AuthenticationStatus.SetupRequired:
                        ModelState.AddModelError("SetupRequired", "Google responded to a additional setup is required.");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                ModelState.AddModelError("NoOpenIDIdentifier", "No OpenID identifier was provided");
            }

            return RedirectToAction(MVC.Accounts.LogIn(validatedReturnUrl));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Xrds()
        {
            var model = new XrdsModel();

            return View(model);
        }

        #endregion
    }
}