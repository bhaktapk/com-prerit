using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using Com.Prerit.Domain;

using Links;

namespace Com.Prerit.Services
{
    public class RoleService : IRoleService
    {
        #region Fields

        private readonly ICacheService _cacheService;

        private readonly HttpServerUtilityBase _server;

        private readonly IXmlStoreService _xmlStoreService;

        private static readonly object RoleDictionarySyncRoot = new object();

        private static readonly Dictionary<RoleType, object> RoleSyncRoots = new Dictionary<RoleType, object>();

        #endregion

        #region Constructors

        public RoleService(ICacheService cacheService, IXmlStoreService xmlStoreService, HttpServerUtilityBase server)
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

        public IEnumerable<string> GetIdsByRole(RoleType roleType)
        {
            return GetRole(roleType).Ids;
        }

        private object GetProfileSyncRoot(RoleType roleType)
        {
            if (!RoleSyncRoots.ContainsKey(roleType))
            {
                lock (RoleDictionarySyncRoot)
                {
                    if (!RoleSyncRoots.ContainsKey(roleType))
                    {
                        RoleSyncRoots.Add(roleType, new object());
                    }
                }
            }

            return RoleSyncRoots[roleType];
        }

        private Role GetRole(RoleType roleType)
        {
            if (_cacheService.GetRole(roleType) == null)
            {
                lock (GetProfileSyncRoot(roleType))
                {
                    if (_cacheService.GetRole(roleType) == null)
                    {
                        string fileName = Enum.GetName(typeof(RoleType), roleType) + ".xml";

                        string fileVirtualPath = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(App_Data.Roles.Url()), fileName);

                        string filePath = _server.MapPath(fileVirtualPath);

                        Role role = File.Exists(filePath)
                                        ? _xmlStoreService.Load<Role>(filePath)
                                        : new Role
                                              {
                                                  Ids = new List<string>(),
                                                  Type = roleType
                                              };

                        _cacheService.SetRole(role, filePath);
                    }
                }
            }

            return _cacheService.GetRole(roleType);
        }

        public IEnumerable<RoleType> GetRolesById(string id)
        {
            return from RoleType knownRole in Enum.GetValues(typeof(RoleType))
                   where GetRole(knownRole).Ids.Contains(id)
                   select knownRole;
        }

        #endregion
    }
}