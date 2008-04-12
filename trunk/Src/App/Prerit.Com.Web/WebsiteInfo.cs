namespace Prerit.Com.Web
{
    public static class WebsiteInfo
    {
        public const int DomainRegistrationYear = 2002;

        public const string Author = "Prerit Bhakta";

        public const string AuthorEmailAddress = "prerit.bhakta@gmail.com";

        public const string GoogleVerificationMetaTagName = "verify-v1";

        public const string GoogleVerificationMetaTagContent = "Zh9YODQtJIOw8hV64WsjasfjjqakXRWrCtzfe0XD/3Q=";

        public const string SiteName = "prerit.com";

        public const string SmtpHost = "relay-hosting.secureserver.net";

        public static string GetContactEmailSubject(string userName)
        {
            return string.Format("{0} user, '{1}', is contacting you", SiteName, userName);
        }

        public static string FormatPageTitle(string pageTitle)
        {
            return string.Format("prerit.com | {0}", pageTitle ?? "");
        }
    } 
}