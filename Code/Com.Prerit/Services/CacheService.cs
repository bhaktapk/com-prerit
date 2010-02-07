using System;
using System.Web.Caching;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public class CacheService : ICacheService
    {
        #region Constants

        private const string ProfileKeyBase = "Profile-{0}";

        private const string RoleKeyBase = "Role-{0}";

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

        private string CreateProfileKey(string id)
        {
            return string.Format(ProfileKeyBase, id);
        }

        private string CreateRoleKey(RoleType roleType)
        {
            return string.Format(RoleKeyBase, Enum.GetName(typeof(RoleType), roleType));
        }

        public Profile GetProfile(string id)
        {
            return _cache[CreateProfileKey(id)] as Profile;
        }

        public Role GetRole(RoleType roleType)
        {
            return _cache[CreateRoleKey(roleType)] as Role;
        }

        public void SetProfile(Profile profile, string filePath)
        {
            _cache.Insert(CreateProfileKey(profile.Id), profile, new CacheDependency(filePath));
        }

        public void SetRole(Role role, string filePath)
        {
            _cache.Insert(CreateRoleKey(role.Type), role, new CacheDependency(filePath));
        }

        #endregion
    }
}