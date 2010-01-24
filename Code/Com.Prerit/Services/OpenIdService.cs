using System;
using System.Web;
using System.Web.Security;

using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace Com.Prerit.Services
{
    public class OpenIdService : IOpenIdService
    {
        #region Fields

        private readonly IMembershipService _membershipService;

        private readonly HttpRequestBase _request;

        #endregion

        #region Constructors

        public OpenIdService(IMembershipService membershipService, HttpRequestBase request)
        {
            if (membershipService == null)
            {
                throw new ArgumentNullException("membershipService");
            }

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            _membershipService = membershipService;
            _request = request;
        }

        #endregion

        #region Methods

        public IAuthenticationRequest CreateRequest(string returnUrl)
        {
            if (!Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
            {
                throw new ArgumentException("Value is not a well formed relative uri string", "returnUrl");
            }

            IAuthenticationRequest request;

            var baseUri = new Uri(_request.Url, "/");

            var returnToUri = new Uri(baseUri, returnUrl);

            using (var openIdRelyingParty = new OpenIdRelyingParty())
            {
                request = openIdRelyingParty.CreateRequest(Identifier.Parse("https://www.google.com/accounts/o8/id"), new Realm(baseUri), returnToUri);
            }

            request.AddExtension(new ClaimsRequest { Email = DemandLevel.Require });

            return request;
        }

        public IAuthenticationResponse GetResponse()
        {
            IAuthenticationResponse response;

            using (var openid = new OpenIdRelyingParty())
            {
                response = openid.GetResponse();
            }

            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        var claimsResponse = response.GetExtension<ClaimsResponse>();

                        _membershipService.SaveAccount(response.ClaimedIdentifier, claimsResponse.Email);

                        FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, false);

                        break;
                }
            }

            return response;
        }

        #endregion
    }
}