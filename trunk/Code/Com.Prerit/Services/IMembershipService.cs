using Com.Prerit.Domain;

using DotNetOpenAuth.OpenId;

namespace Com.Prerit.Services
{
    public interface IMembershipService
    {
        #region Methods

        Account GetAccount(Identifier claimedIdentifier);

        void SaveAccount(Identifier claimedIdentifier, string emailAddress);

        #endregion
    }
}