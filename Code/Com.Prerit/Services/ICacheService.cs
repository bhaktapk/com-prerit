using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface ICacheService
    {
        #region Methods

        Profile GetProfile(string id);

        Role GetRole(RoleType roleType);

        void SetProfile(Profile profile, string filePath);

        void SetRole(Role role, string filePath);

        #endregion
    }
}