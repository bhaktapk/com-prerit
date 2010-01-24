using System;
using System.Web.Mvc;

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

        private readonly IOpenIdService _openIdService;

        #endregion

        #region Constructors

        public OpenIdController(IOpenIdService openIdService)
        {
            if (openIdService == null)
            {
                throw new ArgumentNullException("openIdService");
            }

            _openIdService = openIdService;
        }

        #endregion

        #region Methods

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("Request")]
        public virtual ActionResult RequestAuth(string returnUrl)
        {
            string validatedReturnUrl = Uri.IsWellFormedUriString(returnUrl, UriKind.Relative) ? returnUrl : null;

            IAuthenticationRequest request = _openIdService.CreateRequest(Url.Action(MVC.OpenId.Respond(validatedReturnUrl)));

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
                        if (!string.IsNullOrEmpty(validatedReturnUrl))
                        {
                            return Redirect(validatedReturnUrl);
                        }

                        return RedirectToAction(MVC.About.Index());
                    default:
                        ModelState.AddModelError(response.Exception.Message, response.Exception.Message);
                        break;
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