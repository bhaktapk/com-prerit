using System.Web.Mvc;

namespace Com.Prerit.Helpers.Contact
{
    public static class IndexHelper
    {
        #region Methods

        public static void RenderValidationSummaryPartial(this HtmlHelper helper)
        {
            if (!helper.ViewData.ModelState.IsValid)
            {
                helper.RenderValidationSummary();
            }
        }

        #endregion
    }
}