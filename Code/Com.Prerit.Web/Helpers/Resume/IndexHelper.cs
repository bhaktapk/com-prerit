using System.Web.Mvc;

namespace Com.Prerit.Web.Helpers.Resume
{
    public static class IndexHelper
    {
        #region Methods

        public static string PrintCss(this UrlHelper helper)
        {
            return helper.Content("~/content/styles/resume/print.css");
        }

        public static string ScreenCss(this UrlHelper helper)
        {
            return helper.Content("~/content/styles/resume/screen.css");
        }

        #endregion
    }
}