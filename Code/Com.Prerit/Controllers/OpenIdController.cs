using System;
using System.Web.Mvc;
using System.Web.Security;

using Com.Prerit.Models.OpenId;
using Com.Prerit.Services;

using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace Com.Prerit.Controllers
{
    public partial class OpenIdController : Controller
    {
        #region Fields

        private readonly IMembershipService _membershipService;

        private readonly IOpenIdService _openIdService;

        #endregion

        #region Constructors

        public OpenIdController(IMembershipService membershipService, IOpenIdService openIdService)
        {
            if (membershipService == null)
            {
                throw new ArgumentNullException("membershipService");
            }

            if (openIdService == null)
            {
                throw new ArgumentNullException("openIdService");
            }

            _membershipService = membershipService;
            _openIdService = openIdService;
        }

        #endregion

        #region Methods

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("Request")]
        public virtual ActionResult RequestAuth(string returnUrl)
        {
            string validatedReturnUrl = Uri.IsWellFormedUriString(returnUrl, UriKind.RelativeOrAbsolute) ? returnUrl : null;

            var baseUri = new Uri(Request.Url, "/");

            var returnToUri = new Uri(baseUri, Url.Action(MVC.OpenId.Respond(validatedReturnUrl)));

            IAuthenticationRequest request = _openIdService.CreateRequest(baseUri, returnToUri);

            return request.RedirectingResponse.AsActionResult();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Respond(string returnUrl)
        {
            string validatedReturnUrl = Uri.IsWellFormedUriString(returnUrl, UriKind.RelativeOrAbsolute) ? returnUrl : null;

            IAuthenticationResponse response = _openIdService.GetResponse();

            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    var claimsResponse = response.GetExtension<ClaimsResponse>();

                    _membershipService.SaveAccount(response.ClaimedIdentifier, claimsResponse.Email);

                    FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, false);

                    if (!string.IsNullOrEmpty(validatedReturnUrl))
                    {
                        return Redirect(validatedReturnUrl);
                    }

                    return RedirectToAction(MVC.About.Index());
                default:
                    ModelState.AddModelError(response.Exception.Message, response.Exception.Message);

                    return RedirectToAction(MVC.Accounts.LogIn(validatedReturnUrl));
            }
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