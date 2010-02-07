using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Security;

using Com.Prerit.Domain;
using Com.Prerit.Services;

using Microsoft.Practices.ServiceLocation;

namespace Com.Prerit.Infrastructure.Providers
{
    public class RoleTypeProvider : RoleProvider
    {
        #region Fields

        private readonly IRoleService _roleService;

        #endregion

        #region Properties

        public override string ApplicationName { get; set; }

        #endregion

        #region Constructors

        public RoleTypeProvider()
            : this(ServiceLocator.Current.GetInstance<IRoleService>())
        {
        }

        public RoleTypeProvider(IRoleService roleService)
        {
            if (roleService == null)
            {
                throw new ArgumentNullException("roleService");
            }

            _roleService = roleService;
        }

        #endregion

        #region Methods

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return Enum.GetNames(typeof(RoleType));
        }

        public override string[] GetRolesForUser(string username)
        {
            return (from roleType in _roleService.GetRolesById(username)
                    select Enum.GetName(typeof(RoleType), roleType)).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            RoleType roleType;

            if (TryParseRoleName(roleName, out roleType))
            {
                return _roleService.GetIdsByRole(roleType).ToArray();
            }

            return new string[0];
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = typeof(RoleTypeProvider).Name;
            }

            base.Initialize(name, config);

            if (string.IsNullOrEmpty(config["applicationName"]))
            {
                throw new ProviderException(string.Format("Key applicationName is required"));
            }

            ApplicationName = config["applicationName"];

            config.Remove("applicationName");

            if (config.HasKeys())
            {
                throw new ProviderException(string.Format("Unrecognized key {0}", config.GetKey(0)));
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            RoleType roleType;

            if (TryParseRoleName(roleName, out roleType))
            {
                return _roleService.GetRolesById(username).Contains(roleType);
            }

            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return Enum.GetNames(typeof(RoleType)).Contains(roleName, StringComparer.OrdinalIgnoreCase);
        }

        private bool TryParseRoleName(string roleName, out RoleType roleType)
        {
            if (RoleExists(roleName))
            {
                roleType = (RoleType) Enum.Parse(typeof(RoleType), roleName);

                return true;
            }

            roleType = 0;

            return false;
        }

        #endregion
    }
}