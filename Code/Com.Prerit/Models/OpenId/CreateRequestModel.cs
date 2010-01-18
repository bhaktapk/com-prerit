using Castle.Components.Validator;

using Com.Prerit.Core;

namespace Com.Prerit.Models.OpenId
{
    public class CreateRequestModel
    {
        #region Properties

        [ValidateNonEmpty("An OpenID identifier is required")]
        public string OpenIdIdentifier { get; set; }

        public string OpenIdUsername { get; set; }

        public string ReturnUrl { get; set; }

        #endregion

        #region Nested Type: PropertyName

        public static class PropertyName
        {
            #region Fields

            public static readonly string OpenIdIdentifier = Reflect<CreateRequestModel>.GetProperty(i => i.OpenIdIdentifier).Name;

            public static readonly string OpenIdUsername = Reflect<CreateRequestModel>.GetProperty(i => i.OpenIdUsername).Name;

            public static readonly string ReturnUrl = Reflect<CreateRequestModel>.GetProperty(i => i.ReturnUrl).Name;

            #endregion
        }

        #endregion
    }
}