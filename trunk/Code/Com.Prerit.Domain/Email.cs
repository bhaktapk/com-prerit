namespace Com.Prerit.Domain
{
    public class Email
    {
        #region Properties

        public string FromEmailAddress { get; set; }

        public string Message { get; set; }

        public string Subject { get; set; }

        public string ToEmailAddress { get; set; }

        #endregion
    }
}