using Com.Prerit.Domain;

namespace Com.Prerit.Controllers.Services
{
    public interface ISessionService
    {
        #region Methods

        Account Account { get; set; }

        #endregion
    }
}