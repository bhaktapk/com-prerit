using System.Web;

namespace Prerit.Com.Web
{
    public static class ContentQualityComLink
    {
        #region Methods

        public static string GetSection508ValidatorUrl(string url)
        {
            return string.Format("http://www.contentquality.com/mynewtester/cynthia.exe?rptmode=-1&url1={0}", HttpUtility.UrlEncode(url));
        }

        public static string GetWaiValidatorUrl(string url)
        {
            return string.Format("http://www.contentquality.com/mynewtester/cynthia.exe?rptmode=2&url1={0}", HttpUtility.UrlEncode(url));
        }

        #endregion
    }
}