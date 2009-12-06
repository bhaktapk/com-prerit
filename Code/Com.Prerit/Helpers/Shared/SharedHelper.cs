using System;
using System.Web.Mvc;

namespace Com.Prerit.Web.Helpers.Shared
{
    public static class SharedHelper
    {
        #region Methods

        public static string HtmlizeLineBreaks(this HtmlHelper helper, string s)
        {
            return s.Replace(Environment.NewLine, "<br />");
        }

        public static string PrintCss(this UrlHelper helper)
        {
            return helper.Content("~/content/styles/print.css");
        }

        public static string ScreenCss(this UrlHelper helper)
        {
            return helper.Content("~/content/styles/screen.css");
        }

        #endregion
    }
}