using System.Collections.Generic;

using Com.Prerit.Domain;

using DotNetOpenAuth.OpenId;

namespace Com.Prerit.Services
{
    public interface ICacheService
    {
        #region Methods

        Account GetAccount(Identifier id);

        IEnumerable<Account> GetAdminAccounts();

        void SetAccount(Account value, Identifier id, string filePath);

        void SetAdminAccounts(IEnumerable<Account> value, string filePath);

        #endregion
    }
}