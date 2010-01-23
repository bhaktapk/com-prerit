using System;

using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace Com.Prerit.Controllers.Services
{
    public class OpenIdService : IOpenIdService
    {
        #region Methods

        public IAuthenticationRequest CreateRequest(Uri baseUri, Uri returnToUri)
        {
            IAuthenticationRequest request;

            using (var openIdRelyingParty = new OpenIdRelyingParty())
            {
                request = openIdRelyingParty.CreateRequest(Identifier.Parse("https://www.google.com/accounts/o8/id"), new Realm(baseUri), returnToUri);

                request.AddExtension(new ClaimsRequest { Email = DemandLevel.Require });
            }

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