using System;
using System.Collections.Generic;
using System.IO;
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

        private static readonly object RoleDictionarySyncRoot = new object();

        private static readonly Dictionary<string, object> RoleSyncRoots = new Dictionary<string, object>();

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

        public IEnumerable<string> GetIdsByRole(string roleName)
        {
            string filePath = _server.MapPath(VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(App_Data.Roles.Url()), roleName + ".xml"));

            if (!File.Exists(filePath))
            {
                return new string[0];
            }

            if (_cacheService.GetRole(roleName) == null)
            {
                lock (GetProfileSyncRoot(roleName))
                {
                    if (_cacheService.GetRole(roleName) == null)
                    {
                        _cacheService.SetRole(_xmlStoreService.Load<Role>(filePath), filePath);
                    }
                }
            }

            return _cacheService.GetRole(roleName).Ids;
        }

        private object GetProfileSyncRoot(string roleName)
        {
            if (!RoleSyncRoots.ContainsKey(roleName))
            {
                lock (RoleDictionarySyncRoot)
                {
                    if (!RoleSyncRoots.ContainsKey(roleName))
                    {
                        RoleSyncRoots.Add(roleName, new object());
                    }
                }
            }

            return RoleSyncRoots[roleName];
        }

        #endregion
    }
}