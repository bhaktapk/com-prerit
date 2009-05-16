using Castle.Components.Validator;

using Com.Prerit.Core;
using Com.Prerit.Web.Models.Shared;

namespace Com.Prerit.Web.Models.Contact
{
    public class IndexModel : DefaultMasterModel
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

            public static readonly string EmailAddress = TypeUtil.GetMemberName<IndexModel>(m => m.EmailAddress);

            public static readonly string Message = TypeUtil.GetMemberName<IndexModel>(m => m.Message);

            public static readonly string Name = TypeUtil.GetMemberName<IndexModel>(m => m.Name);

            #endregion
        }

        #endregion
    }
}