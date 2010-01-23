using System;
using System.Web;

using Com.Prerit.Domain;

namespace Com.Prerit.Controllers.Services
{
    public class SessionService : ISessionService
    {
        #region Constants

        private const string AccountSessionKey = "Account";

        #endregion

        #region Fields

        private readonly HttpSessionStateBase _session;

        #endregion

        #region Properties

        public Account Account
        {
            get { return _session[AccountSessionKey] as Account; }
            set { _session[AccountSessionKey] = value; }
        }

        #endregion

        #region Constructors

        public SessionService(HttpSessionStateBase session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            _session = session;
        }

        #endregion
    }
}