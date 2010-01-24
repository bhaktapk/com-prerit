using System.Web.Security;

namespace Com.Prerit.Services
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        #region Properties

        public string DefaultUrl
        {
            get { return FormsAuthentication.DefaultUrl; }
        }

        #endregion

        #region Methods

        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        #endregion
    }
}