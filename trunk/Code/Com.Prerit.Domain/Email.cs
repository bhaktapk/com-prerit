using Castle.Components.Validator;

namespace Com.Prerit.Domain
{
    public class Email
    {
        #region Properties

        [ValidateNonEmpty("A from e-mail address is required")]
        [ValidateEmail("The from e-mail address is not valid")]
        public string FromEmailAddress { get; set; }

        [ValidateNonEmpty("A message is required")]
        public string Message { get; set; }

        [ValidateNonEmpty("A subject is required")]
        public string Subject { get; set; }

        [ValidateNonEmpty("A to e-mail address is required")]
        [ValidateEmail("The to e-mail address is not valid")]
        public string ToEmailAddress { get; set; }

        #endregion
    }
}