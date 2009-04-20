namespace Com.Prerit.Services
{
    public interface IEmailSenderService
    {
        void Send(string fromEmailAddress, string toEmailAddress, string subject, string body);
    }
}