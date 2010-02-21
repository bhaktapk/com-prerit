using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Com.Prerit.Core;
using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public class RoleService : IRoleService
    {
        #region Fields

        private readonly ICacheService _cacheService;

        private readonly IDiskInputOutputService _diskInputOutputService;

        private readonly string _rolesDirectoryPath;

        private static readonly object RoleDictionarySyncRoot = new object();

        private static readonly Dictionary<RoleType, object> RoleSyncRoots = new Dictionary<RoleType, object>();

        #endregion

        #region Constructors

        public RoleService(string rolesDirectoryPath, ICacheService cacheService, IDiskInputOutputService diskInputOutputService)
        {
            if (rolesDirectoryPath == null)
            {
                throw new ArgumentNullException("rolesDirectoryPath");
            }

            if (cacheService == null)
            {
                throw new ArgumentNullException("cacheService");
            }

            if (diskInputOutputService == null)
            {
                throw new ArgumentNullException("diskInputOutputService");
            }

            _rolesDirectoryPath = rolesDirectoryPath;
            _cacheService = cacheService;
            _diskInputOutputService = diskInputOutputService;
        }

        #endregion

        #region Methods

        private object AddRoleSyncRoot(RoleType roleType)
        {
            var roleSyncRoot = new object();

            RoleSyncRoots.Add(roleType, roleSyncRoot);

            return roleSyncRoot;
        }

        public IEnumerable<string> GetIdsByRole(RoleType roleType)
        {
            return GetRole(roleType).Ids;
        }

        private Role GetRole(RoleType roleType)
        {
            Role role = _cacheService.GetRole(roleType);

            if (role != null)
            {
                return role;
            }

            lock (GetRoleSyncRoot(roleType))
            {
                return _cacheService.GetRole(roleType) ?? LoadRole(roleType);
            }
        }

        public IEnumerable<RoleType> GetRolesById(string id)
        {
            return (from RoleType knownRole in Enum.GetValues(typeof(RoleType))
                    where GetRole(knownRole).Ids.Contains(id)
                    select knownRole).ExecuteQuery();
        }

        private object GetRoleSyncRoot(RoleType roleType)
        {
            object roleSyncRoot;

            if (RoleSyncRoots.TryGetValue(roleType, out roleSyncRoot))
            {
                return roleSyncRoot;
            }

            lock (RoleDictionarySyncRoot)
            {
                if (RoleSyncRoots.TryGetValue(roleType, out roleSyncRoot))
                {
                    return roleSyncRoot;
                }

                return AddRoleSyncRoot(roleType);
            }
        }

        private Role LoadRole(RoleType roleType)
        {
            string fileName = Enum.GetName(typeof(RoleType), roleType) + ".xml";

            string filePath = Path.Combine(_rolesDirectoryPath, fileName);

            Role role;

            if (_diskInputOutputService.FileExists(filePath))
            {
                role = _diskInputOutputService.LoadXmlFile<Role>(filePath);
            }
            else
            {
                role = new Role
                           {
                               Ids = new List<string>(),
                               Type = roleType
                           };
            }

            _cacheService.SetRole(role, filePath);

            return role;
        }

        #endregion
    }
}