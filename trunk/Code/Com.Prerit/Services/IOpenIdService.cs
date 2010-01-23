using System;

using DotNetOpenAuth.OpenId.RelyingParty;

namespace Com.Prerit.Services
{
    public interface IOpenIdService
    {
        #region Methods

        IAuthenticationRequest CreateRequest(Uri baseUri, Uri returnToUri);

        IAuthenticationResponse GetResponse();

        #endregion
    }
}