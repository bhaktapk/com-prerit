using System;

namespace Com.Prerit.Web
{
    public static class EmailInfo
    {
        #region Constants

        public const string AuthorEmailAddress = "prerit.bhakta@gmail.com";

        public const string SmtpHost = "relay-hosting.secureserver.net";

        #endregion

        #region Methods

        public static string GetContactEmailSubject(string userName)
        {
            return String.Format("prerit.com user, '{0}', is contacting you", userName);
        }

        #endregion
    }
}