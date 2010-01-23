using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IAccountsService
    {
        #region Methods

        Account GetAccount();

        void SaveAccount(string claimedIdentifier, string emailAddress);

        #endregion
    }
}