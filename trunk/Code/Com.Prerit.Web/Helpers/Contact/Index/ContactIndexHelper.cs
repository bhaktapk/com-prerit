using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Com.Prerit.Web.Helpers.Contact.Index
{
    public static class ContactIndexHelper
    {
        #region Methods

        public static void RenderPartialValidationSummary(this HtmlHelper helper)
        {
            if (!helper.ViewData.ModelState.IsValid)
            {
                helper.RenderPartial(ContactHelper.PartialName.ValidationSummary);
            }
        }

        #endregion
    }
}