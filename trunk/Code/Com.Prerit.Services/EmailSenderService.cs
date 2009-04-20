using System.Net.Mail;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        #region Fields

        private readonly SmtpClient _smtpClient;

        #endregion

        #region Constructors

        public EmailSenderService(string smtpHost)
        {
            _smtpClient = new SmtpClient
                              {
                                  Host = smtpHost
                              };
        }

        #endregion

        #region Methods

        public void Send(Email email)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(email.FromEmailAddress);
                message.To.Add(email.ToEmailAddress);
                message.Subject = email.Subject;
                message.Body = email.Message;
                message.IsBodyHtml = false;

                // TODO: uncomment
                //_smtpClient.Send(message);
            }
        }

        #endregion
    }
}