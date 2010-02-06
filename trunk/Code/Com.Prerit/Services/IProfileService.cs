using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IProfileService
    {
        #region Methods

        Profile GetProfile(string id);

        void SaveProfile(string id, string emailAddress);

        #endregion
    }
}