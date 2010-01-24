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

        private readonly HttpRequestBase _request;

        #endregion

        #region Constructors

        public OpenIdService(HttpRequestBase request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            _request = request;
        }

        #endregion

        #region Methods

        public IAuthenticationRequest CreateRequest(string returnUrl)
        {
            if (!Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
            {
                throw new ArgumentException("Parameter is not a well formed relative uri string", "returnUrl");
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
            using (var openid = new OpenIdRelyingParty())
            {
                return openid.GetResponse();
            }
        }

        #endregion
    }
}