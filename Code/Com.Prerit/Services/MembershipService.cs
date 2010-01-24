using System;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public class MembershipService : IMembershipService
    {
        #region Fields

        private readonly ISessionService _sessionService;

        #endregion

        #region Constructors

        public MembershipService(ISessionService sessionService)
        {
            if (sessionService == null)
            {
                throw new ArgumentNullException("sessionService");
            }

            _sessionService = sessionService;
        }

        #endregion

        #region Methods

        private string CreateName(string emailAddress)
        {
            return emailAddress.Replace("@", " at ").Replace(".", " dot ");
        }

        public Account GetAccount()
        {
            return _sessionService.Account;
        }

        public void SaveAccount(string claimedIdentifier, string emailAddress)
        {
            _sessionService.Account = new Account { ClaimedIdentifier = claimedIdentifier, EmailAddress = emailAddress, Name = CreateName(emailAddress) };
        }

        #endregion
    }
}