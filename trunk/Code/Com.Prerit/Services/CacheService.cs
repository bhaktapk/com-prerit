using System;
using System.Collections.Generic;
using System.Web.Caching;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public class CacheService : ICacheService
    {
        #region Constants

        private const string AdminAccountsKey = "AdminAccounts";

        #endregion

        #region Fields

        private readonly Cache _cache;

        #endregion

        #region Properties

        public IEnumerable<Account> AdminAccounts
        {
            get { return _cache[AdminAccountsKey] as IEnumerable<Account>; }
            set { _cache[AdminAccountsKey] = value; }
        }

        #endregion

        #region Constructors

        public CacheService(Cache cache)
        {
            if (cache == null)
            {
                throw new ArgumentNullException("cache");
            }

            _cache = cache;
        }

        #endregion
    }
}