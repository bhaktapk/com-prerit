using System;

using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace Com.Prerit.Controllers.Services
{
    public class OpenIdService : IOpenIdService
    {
        #region Methods

        public IAuthenticationRequest CreateAuthenticationRequest(string openIdIdentifier, Uri baseUri, Uri returnToUri)
        {
            IAuthenticationRequest request;

            using (var openIdRelyingParty = new OpenIdRelyingParty())
            {
                request = openIdRelyingParty.CreateRequest(Identifier.Parse(openIdIdentifier), new Realm(baseUri), returnToUri);
            }

            return request;
        }

        public IAuthenticationResponse GetAuthenticationResponse()
        {
            using (var openid = new OpenIdRelyingParty())
            {
                return openid.GetResponse();
            }
        }

        #endregion
    }
}