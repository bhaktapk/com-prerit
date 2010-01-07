using Castle.Components.Validator;

namespace Com.Prerit.Models.Contact
{
    public class IndexModel
    {
        #region Properties

        [ValidateNonEmpty("An e-mail is required")]
        [ValidateEmail("The e-mail address is not valid")]
        public string EmailAddress { get; set; }

        [ValidateNonEmpty("A message is required")]
        public string Message { get; set; }

        [ValidateNonEmpty("A name is required")]
        public string Name { get; set; }

        #endregion
    }
}