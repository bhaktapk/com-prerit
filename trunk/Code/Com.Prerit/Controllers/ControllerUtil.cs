using System;
using System.Web.Mvc;

using Castle.Components.Validator;

namespace Com.Prerit.Controllers
{
    public static class ControllerUtil
    {
        #region Methods

        public static void AddModelErrors(ModelStateDictionary modelState, ValidationException exception)
        {
            foreach (string errorMessage in exception.ValidationErrorMessages)
            {
                modelState.AddModelError(errorMessage, errorMessage);
            }
        }

        public static bool IsLoginRequest(Uri requestUrl, UrlHelper urlHelper)
        {
            string relativeLoginUrl = urlHelper.Action(MVC.Accounts.LogIn(null));

            var absoluteLoginUrl = new Uri(requestUrl, relativeLoginUrl);

            return string.Compare(requestUrl.LocalPath, absoluteLoginUrl.LocalPath, StringComparison.OrdinalIgnoreCase) == 0;
        }

        #endregion
    }
}