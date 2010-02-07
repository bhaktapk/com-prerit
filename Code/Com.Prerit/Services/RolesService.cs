using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private static readonly Dictionary<KnownRole, object> RoleSyncRoots = new Dictionary<KnownRole, object>();

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

        public IEnumerable<string> GetIdsByRole(KnownRole knownRole)
        {
            return GetRole(knownRole).Ids;
        }

        private object GetProfileSyncRoot(KnownRole knownRole)
        {
            if (!RoleSyncRoots.ContainsKey(knownRole))
            {
                lock (RoleDictionarySyncRoot)
                {
                    if (!RoleSyncRoots.ContainsKey(knownRole))
                    {
                        RoleSyncRoots.Add(knownRole, new object());
                    }
                }
            }

            return RoleSyncRoots[knownRole];
        }

        private Role GetRole(KnownRole knownRole)
        {
            if (_cacheService.GetRole(knownRole) == null)
            {
                lock (GetProfileSyncRoot(knownRole))
                {
                    if (_cacheService.GetRole(knownRole) == null)
                    {
                        string roleName = Enum.GetName(typeof(KnownRole), knownRole);

                        string fileVirtualPath = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(App_Data.Roles.Url()), roleName + ".xml");

                        string filePath = _server.MapPath(fileVirtualPath);

                        Role role = File.Exists(filePath)
                                        ? _xmlStoreService.Load<Role>(filePath)
                                        : new Role
                                              {
                                                  Ids = new List<string>(),
                                                  Name = roleName
                                              };

                        _cacheService.SetRole(role, filePath);
                    }
                }
            }

            return _cacheService.GetRole(knownRole);
        }

        public IEnumerable<KnownRole> GetRolesById(string id)
        {
            return from KnownRole knownRole in Enum.GetValues(typeof(KnownRole))
                   where GetRole(knownRole).Ids.Contains(id)
                   select knownRole;
        }

        #endregion
    }
}