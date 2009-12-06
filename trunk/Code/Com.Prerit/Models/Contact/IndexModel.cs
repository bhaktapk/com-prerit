using Castle.Components.Validator;

using Com.Prerit.Core;

namespace Com.Prerit.Web.Models.Contact
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

        #region Nested Type: PropertyName

        public static class PropertyName
        {
            #region Fields

            public static readonly string EmailAddress = Reflect<IndexModel>.GetProperty(i => i.EmailAddress).Name;

            public static readonly string Message = Reflect<IndexModel>.GetProperty(i => i.Message).Name;

            public static readonly string Name = Reflect<IndexModel>.GetProperty(i => i.Name).Name;

            #endregion
        }

        #endregion
    }
}