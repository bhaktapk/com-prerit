using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Com.Prerit.Helpers.Contact
{
    public static class ValidationSummaryHelper
    {
        #region Methods

        public static void RenderValidationSummary(this HtmlHelper helper)
        {
            helper.RenderPartial(MVC.Contact.Views.ValidationSummary);
        }

        public static void RepeatErrorMessages(this HtmlHelper helper, Action<ModelError> render)
        {
            foreach (ModelState modelState in helper.ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    render(error);
                }
            }
        }

        #endregion
    }
}