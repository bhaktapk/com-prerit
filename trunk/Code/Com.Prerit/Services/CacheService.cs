using System;
using System.Collections.Generic;
using System.Web.Caching;

using Com.Prerit.Domain;

using DotNetOpenAuth.OpenId;

namespace Com.Prerit.Services
{
    public class CacheService : ICacheService
    {
        #region Constants

        private const string AccountKeyBase = "Account-{0}";

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

        private string CreateAccountKey(Identifier id)
        {
            return string.Format(AccountKeyBase, id);
        }

        public Account GetAccount(Identifier id)
        {
            return _cache[CreateAccountKey(id)] as Account;
        }

        public IEnumerable<Account> GetAdminAccounts()
        {
            return _cache[AdminAccountsKey] as IEnumerable<Account>;
        }

        public void SetAccount(Account value, Identifier id, string filePath)
        {
            _cache.Insert(CreateAccountKey(id), value, new CacheDependency(filePath));
        }

        public void SetAdminAccounts(IEnumerable<Account> value, string filePath)
        {
            _cache.Insert(AdminAccountsKey, value, new CacheDependency(filePath));
        }

        #endregion
    }
}