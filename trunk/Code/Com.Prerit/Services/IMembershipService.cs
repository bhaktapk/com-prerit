using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IMembershipService
    {
        #region Methods

        Account GetAccount();

        void SaveAccount(string claimedIdentifier, string emailAddress);

        #endregion
    }
}