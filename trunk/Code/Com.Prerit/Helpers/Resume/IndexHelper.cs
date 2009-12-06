using System.Web.Mvc;

namespace Com.Prerit.Helpers.Resume
{
    public static class IndexHelper
    {
        #region Methods

        public static string PrintCss(this UrlHelper helper)
        {
            return helper.Content("~/content/styles/resume/print.css");
        }

        public static string ResumeDoc(this UrlHelper helper)
        {
            return helper.Content("~/content/resume/resume-of-prerit-bhakta.doc");
        }

        public static string ResumePdf(this UrlHelper helper)
        {
            return helper.Content("~/content/resume/resume-of-prerit-bhakta.pdf");
        }

        public static string ScreenCss(this UrlHelper helper)
        {
            return helper.Content("~/content/styles/resume/screen.css");
        }

        #endregion
    }
}