using System;
using System.Web.Mvc;

namespace Com.Prerit.Web.Helpers.Contact.Index
{
    public static class ContactValidationSummaryHelper
    {
        #region Methods

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