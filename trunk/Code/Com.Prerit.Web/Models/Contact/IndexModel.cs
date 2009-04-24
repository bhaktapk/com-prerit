using Com.Prerit.Core;
using Com.Prerit.Web.Models.Shared;

namespace Com.Prerit.Web.Models.Contact
{
    public class IndexModel : DefaultMasterModel
    {
        #region Properties

        public string EmailAddress { get; set; }

        public string Message { get; set; }

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