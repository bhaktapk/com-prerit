using System;
using System.Net.Mail;

using Castle.Components.Validator;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        #region Fields

        private readonly SmtpClient _smtpClient;

        private readonly IValidatorRunner _validatorRunner;

        #endregion

        #region Constructors

        public EmailSenderService(string smtpHost, IValidatorRunner validatorRunner)
        {
            if (validatorRunner == null)
            {
                throw new ArgumentNullException("validatorRunner");
            }

            _smtpClient = new SmtpClient
                              {
                                  Host = smtpHost
                              };

            _validatorRunner = validatorRunner;
        }

        #endregion

        #region Methods

        public void Send(Email email)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            if (!_validatorRunner.IsValid(email))
            {
                throw new ValidationException("Email is invalid to send", _validatorRunner.GetErrorSummary(email).ErrorMessages);
            }

            using (var message = new MailMessage())
            {
                message.From = new MailAddress(email.FromEmailAddress);
                message.To.Add(email.ToEmailAddress);
                message.Subject = email.Subject;
                message.Body = email.Message;
                message.IsBodyHtml = false;

                _smtpClient.Send(message);
            }
        }

        #endregion
    }
}