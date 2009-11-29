using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Com.Prerit.Web.Helpers.Contact
{
    public static class IndexHelper
    {
        #region Methods

        public static void RenderPartialValidationSummary(this HtmlHelper helper)
        {
            if (!helper.ViewData.ModelState.IsValid)
            {
                helper.RenderPartial(PartialName.ValidationSummary);
            }
        }

        #endregion
    }
}