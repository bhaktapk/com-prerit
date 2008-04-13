using System.Web;

namespace Prerit.Com.Web
{
    public static class W3OrgLink
    {
        public static string GetCssValidatorUrl(string url)
        {
            return string.Format("http://jigsaw.w3.org/css-validator/validator?usermedium=all&uri={0}", HttpUtility.UrlEncode(url));
        }

        public static string GetXhtmlValidatorUrl(string url)
        {
            return string.Format("http://validator.w3.org/check?uri={0}", HttpUtility.UrlEncode(url));
        }
    } 
}