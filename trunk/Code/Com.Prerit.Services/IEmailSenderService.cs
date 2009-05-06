using Castle.Components.Validator;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IEmailSenderService
    {
        #region Methods

        ErrorSummary GetErrorSummaryForInvalidEmail(Email email);

        bool IsEmailValidToSend(Email email);

        void Send(Email email);

        #endregion
    }
}