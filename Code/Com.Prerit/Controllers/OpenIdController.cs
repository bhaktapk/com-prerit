using System;
using System.Web.Mvc;
using System.Web.Security;

using Com.Prerit.Controllers.Services;
using Com.Prerit.Filters;
using Com.Prerit.Models.OpenId;

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
        [ModelToTempData]
        [ModelStateToTempData]
        public virtual ActionResult CreateRequest(CreateRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(MVC.Accounts.Login(model.ReturnUrl));
            }

            var baseUri = new Uri(Request.Url, "/");
            var returnToUri = new Uri(baseUri, Url.Action(MVC.OpenId.HandleResponse(model.ReturnUrl)));

            IAuthenticationRequest request = _openIdService.CreateAuthenticationRequest(model.OpenIdIdentifier, baseUri, returnToUri);

            return request.RedirectingResponse.AsActionResult();
        }

        public virtual ActionResult HandleResponse(string returnUrl)
        {
            IAuthenticationResponse response = _openIdService.GetAuthenticationResponse();

            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, false);

                    return !string.IsNullOrEmpty(returnUrl) ? (ActionResult) Redirect(returnUrl) : RedirectToAction(MVC.About.Index());
                default:
                    ModelState.AddModelError(response.Exception.Message, response.Exception.Message);

                    return RedirectToAction(MVC.Accounts.Login(returnUrl));
            }
        }

        public virtual ActionResult Xrds()
        {
            var model = new XrdsModel();

            return View(model);
        }

        #endregion
    }
}