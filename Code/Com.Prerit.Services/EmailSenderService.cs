using System.Net.Mail;

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

        public void Send(string fromEmailAddress, string toEmailAddress, string subject, string body)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(fromEmailAddress);
                message.To.Add(toEmailAddress);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = false;

                // TODO: uncomment
                //_smtpClient.Send(message);
            }
        }

        #endregion
    }
}