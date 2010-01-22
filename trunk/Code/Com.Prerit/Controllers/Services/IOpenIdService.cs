using System;

using DotNetOpenAuth.OpenId.RelyingParty;

namespace Com.Prerit.Controllers.Services
{
    public interface IOpenIdService
    {
        #region Methods

        IAuthenticationRequest CreateRequest(Uri baseUri, Uri returnToUri);

        IAuthenticationResponse GetResponse();

        #endregion
    }
}