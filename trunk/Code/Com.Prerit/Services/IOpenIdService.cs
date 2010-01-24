using DotNetOpenAuth.OpenId.RelyingParty;

namespace Com.Prerit.Services
{
    public interface IOpenIdService
    {
        #region Methods

        IAuthenticationRequest CreateRequest(string returnUrl);

        IAuthenticationResponse GetResponse();

        #endregion
    }
}