namespace Com.Prerit.Services
{
    public interface IFormsAuthenticationService
    {
        #region Properties

        string DefaultUrl { get; }

        #endregion

        #region Methods

        void SetAuthCookie(string userName, bool createPersistentCookie);

        void SignOut();

        #endregion
    }
}