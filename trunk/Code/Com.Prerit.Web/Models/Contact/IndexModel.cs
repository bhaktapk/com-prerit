using System.ComponentModel.DataAnnotations;

using Com.Prerit.Core;
using Com.Prerit.Web.Models.Shared;

namespace Com.Prerit.Web.Models.Contact
{
    public class IndexModel : DefaultMasterModel
    {
        #region Properties

        [Required(ErrorMessage = "E-mail is required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Message is required")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        #endregion

        #region Nested Type: PropertyName

        public static class PropertyName
        {
            #region Fields

            public static readonly string EmailAddress = TypeUtil.GetPropertyName<IndexModel, string>(m => m.EmailAddress).ToLowerFirstLetter();

            public static readonly string Message = TypeUtil.GetPropertyName<IndexModel, string>(m => m.Message).ToLowerFirstLetter();

            public static readonly string Name = TypeUtil.GetPropertyName<IndexModel, string>(m => m.Name).ToLowerFirstLetter();

            #endregion
        }

        #endregion
    }
}