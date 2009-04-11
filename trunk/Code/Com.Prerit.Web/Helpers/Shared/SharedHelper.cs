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

        #endregion
    }
}