using System.Web.Mvc;

namespace Com.Prerit.Web.Helpers.Contact.Index
{
    public static class ContactIndexHelper
    {
        #region Methods

        public static bool IsValidationSummaryVisible(this HtmlHelper helper)
        {
            return !helper.ViewData.ModelState.IsValid;
        }

        #endregion
    }
}