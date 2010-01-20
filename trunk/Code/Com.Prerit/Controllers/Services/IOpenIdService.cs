using System;

using DotNetOpenAuth.OpenId.RelyingParty;

namespace Com.Prerit.Controllers.Services
{
    public interface IOpenIdService
    {
        #region Methods

        IAuthenticationRequest CreateAuthenticationRequest(string openIdIdentifier, Uri baseUri, Uri returnToUri);

        IAuthenticationResponse GetAuthenticationResponse();

        #endregion
    }
}