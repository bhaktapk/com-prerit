namespace Com.Prerit.Web
{
    public static class WebsiteInfo
    {
        #region Constants

        public const string Author = "Prerit Bhakta";

        public const string AuthorEmailAddress = "prerit.bhakta@gmail.com";

        public const int DomainRegistrationYear = 2002;

        public const string SiteName = "prerit.com";

        public const string SmtpHost = "relay-hosting.secureserver.net";

        #endregion

        #region Methods

        public static string FormatPageTitle(string pageTitle)
        {
            return string.Format("prerit.com | {0}", pageTitle ?? "");
        }

        public static string GetContactEmailSubject(string userName)
        {
            return string.Format("{0} user, '{1}', is contacting you", SiteName, userName);
        }

        #endregion
    }
}