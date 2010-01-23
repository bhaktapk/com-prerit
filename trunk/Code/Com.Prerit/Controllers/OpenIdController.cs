using System;
using System.Web.Mvc;
using System.Web.Security;

using Com.Prerit.Controllers.Services;
using Com.Prerit.Domain;
using Com.Prerit.Models.OpenId;

using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace Com.Prerit.Controllers
{
    public partial class OpenIdController : Controller
    {
        #region Fields

        private readonly IAccountsService _accountsService;

        private readonly IOpenIdService _openIdService;

        #endregion

        #region Constructors

        public OpenIdController(IAccountsService accountsService, IOpenIdService openIdService)
        {
            if (accountsService == null)
            {
                throw new ArgumentNullException("accountsService");
            }

            if (openIdService == null)
            {
                throw new ArgumentNullException("openIdService");
            }

            _accountsService = accountsService;
            _openIdService = openIdService;
        }

        #endregion

        #region Methods

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("Request")]
        public virtual ActionResult RequestAuth(string returnUrl)
        {
            var baseUri = new Uri(Request.Url, "/");
            var returnToUri = new Uri(baseUri, Url.Action(MVC.OpenId.Respond(returnUrl)));

            IAuthenticationRequest request = _openIdService.CreateRequest(baseUri, returnToUri);

            return request.RedirectingResponse.AsActionResult();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult Respond(string returnUrl)
        {
            IAuthenticationResponse response = _openIdService.GetResponse();

            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    var claimsResponse = response.GetExtension<ClaimsResponse>();

                    FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, false);

                    _accountsService.SaveAccount(response.ClaimedIdentifier, claimsResponse.Email);

                    return !string.IsNullOrEmpty(returnUrl) ? (ActionResult) Redirect(returnUrl) : RedirectToAction(MVC.About.Index());
                default:
                    ModelState.AddModelError(response.Exception.Message, response.Exception.Message);

                    return RedirectToAction(MVC.Accounts.Login(returnUrl));
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