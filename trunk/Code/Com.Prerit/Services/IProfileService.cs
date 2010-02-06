using Com.Prerit.Domain;

using DotNetOpenAuth.OpenId;

namespace Com.Prerit.Services
{
    public interface IProfileService
    {
        #region Methods

        Account GetAccount(Identifier id);

        void SaveAccount(Identifier id, string emailAddress);

        #endregion
    }
}