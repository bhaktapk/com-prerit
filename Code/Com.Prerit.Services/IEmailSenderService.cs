using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IEmailSenderService
    {
        #region Methods

        void Send(Email email);

        #endregion
    }
}