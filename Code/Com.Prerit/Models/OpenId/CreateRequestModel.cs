using Castle.Components.Validator;

using Com.Prerit.Core;

namespace Com.Prerit.Models.OpenId
{
    public class CreateRequestModel
    {
        #region Properties

        [ValidateNonEmpty("An OpenID identifier is required")]
        public string openid_identifier { get; set; }

        public string openid_username { get; set; }

        public string ReturnUrl { get; set; }

        #endregion

        #region Nested Type: PropertyName

        public static class PropertyName
        {
            #region Fields

            public static readonly string openid_identifier = Reflect<CreateRequestModel>.GetProperty(i => i.openid_identifier).Name;

            public static readonly string openid_username = Reflect<CreateRequestModel>.GetProperty(i => i.openid_username).Name;

            public static readonly string ReturnUrl = Reflect<CreateRequestModel>.GetProperty(i => i.ReturnUrl).Name;

            #endregion
        }

        #endregion
    }
}