using System;
using System.Collections.Generic;
using System.Web;

using Com.Prerit.Domain;

using Links;

namespace Com.Prerit.Services
{
    public class RolesService : IRolesService
    {
        #region Fields

        private readonly ICacheService _cacheService;

        private readonly HttpServerUtilityBase _server;

        private readonly IXmlStoreService _xmlStoreService;

        private static readonly object AdminAccountsSyncRoot = new object();

        #endregion

        #region Constructors

        public RolesService(ICacheService cacheService, IXmlStoreService xmlStoreService, HttpServerUtilityBase server)
        {
            if (cacheService == null)
            {
                throw new ArgumentNullException("cacheService");
            }

            if (xmlStoreService == null)
            {
                throw new ArgumentNullException("xmlStoreService");
            }

            if (server == null)
            {
                throw new ArgumentNullException("server");
            }

            _cacheService = cacheService;
            _xmlStoreService = xmlStoreService;
            _server = server;
        }

        #endregion

        #region Methods

        public IEnumerable<Account> GetAdminAccounts()
        {
            if (_cacheService.GetAdminAccounts() == null)
            {
                lock (AdminAccountsSyncRoot)
                {
                    if (_cacheService.GetAdminAccounts() == null)
                    {
                        string filePath = _server.MapPath(MembershipData.AdminAccounts_xml);

                        _cacheService.SetAdminAccounts(_xmlStoreService.Load<Account[]>(filePath), filePath);
                    }
                }
            }

            return _cacheService.GetAdminAccounts();
        }

        #endregion
    }
}