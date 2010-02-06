using System.Collections.Generic;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IRolesService
    {
        #region Methods

        IEnumerable<Account> GetAdminAccounts();

        #endregion
    }
}