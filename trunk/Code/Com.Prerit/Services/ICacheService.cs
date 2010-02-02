using System.Collections.Generic;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface ICacheService
    {
        #region Properties

        IEnumerable<Account> AdminAccounts { get; set; }

        #endregion
    }
}