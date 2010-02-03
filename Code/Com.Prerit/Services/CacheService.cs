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

        #region Methods

        public IEnumerable<Account> GetAdminAccounts()
        {
            return _cache[AdminAccountsKey] as IEnumerable<Account>;
        }

        public void SetAdminAccounts(IEnumerable<Account> value, string virtualPath)
        {
            _cache.Insert(AdminAccountsKey, value, new CacheDependency(virtualPath));
        }

        #endregion
    }
}