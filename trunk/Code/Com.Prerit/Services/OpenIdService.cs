using System;
using System.Web;

using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace Com.Prerit.Services
{
    public class OpenIdService : IOpenIdService
    {
        #region Fields

        private readonly IProfileService _profileService;

        private readonly HttpRequestBase _request;

        #endregion

        #region Constructors

        public OpenIdService(IProfileService profileService, HttpRequestBase request)
        {
            if (profileService == null)
            {
                throw new ArgumentNullException("membershipService");
            }

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            _profileService = profileService;
            _request = request;
        }

        #endregion

        #region Methods

        public IAuthenticationRequest CreateRequest(string realmUrl, string returnUrl)
        {
            if (!Uri.IsWellFormedUriString(realmUrl, UriKind.Relative))
            {
                throw new ArgumentException("Value is not a well formed relative uri string", "realmUrl");
            }

            if (!Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
            {
                throw new ArgumentException("Value is not a well formed relative uri string", "returnUrl");
            }

            IAuthenticationRequest request;

            var baseUri = new Uri(_request.Url, "/");

            var realmUri = new Uri(baseUri, realmUrl);

            var returnToUri = new Uri(baseUri, returnUrl);

            using (var openIdRelyingParty = new OpenIdRelyingParty())
            {
                request = openIdRelyingParty.CreateRequest(Identifier.Parse("https://www.google.com/accounts/o8/id"), new Realm(realmUri), returnToUri);
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

            if (response != null && response.Status == AuthenticationStatus.Authenticated)
            {
                var claimsResponse = response.GetExtension<ClaimsResponse>();

                _profileService.SaveAccount(response.ClaimedIdentifier, claimsResponse.Email);
            }

            return response;
        }

        #endregion
    }
}