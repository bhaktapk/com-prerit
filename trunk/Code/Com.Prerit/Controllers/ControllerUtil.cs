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

        #endregion
    }
}