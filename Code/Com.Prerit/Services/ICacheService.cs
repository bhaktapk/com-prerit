using System.Collections.Generic;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface ICacheService
    {
        #region Methods

        IEnumerable<Account> GetAdminAccounts();

        void SetAdminAccounts(IEnumerable<Account> value, string virtualPath);

        #endregion
    }
}