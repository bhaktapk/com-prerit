using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

using Com.Prerit.Domain;

using DotNetOpenAuth.OpenId;

using Links;

namespace Com.Prerit.Services
{
    public class MembershipService : IMembershipService
    {
        #region Fields

        private readonly HttpServerUtilityBase _server;

        private readonly ISessionService _sessionService;
        
        private readonly ICacheService _cacheService;

        #endregion

        #region Constructors

        public MembershipService(ICacheService cacheService, ISessionService sessionService, HttpServerUtilityBase server)
        {
            if (cacheService == null)
            {
                throw new ArgumentNullException("cacheService");
            }

            if (sessionService == null)
            {
                throw new ArgumentNullException("sessionService");
            }

            if (server == null)
            {
                throw new ArgumentNullException("server");
            }

            _cacheService = cacheService;
            _sessionService = sessionService;
            _server = server;
        }

        #endregion

        #region Methods

        private string CreateName(string emailAddress)
        {
            return emailAddress.Replace("@", " at ").Replace(".", " dot ");
        }

        public Account GetAccount(Identifier claimedIdentifier)
        {
            if (_sessionService.Account == null)
            {
                Account account;

                var serializer = new XmlSerializer(typeof(Account));

                using (var reader = new StreamReader(GetSavedAccountFilePath(claimedIdentifier)))
                {
                    account = (Account) serializer.Deserialize(reader);
                }

                _sessionService.Account = account;
            }

            return _sessionService.Account;
        }

        private string GetSafeFilename(Identifier claimedIdentifier)
        {
            char[] characters = claimedIdentifier.OriginalString.ToCharArray();

            List<char> safeFilename = characters.Select(c => !Path.GetInvalidFileNameChars().Contains(c) ? c : '-').ToList();

            safeFilename.AddRange(".log".ToCharArray());

            return new string(safeFilename.ToArray());
        }

        private string GetSavedAccountFilePath(Identifier claimedIdentifier)
        {
            string directoryPath = _server.MapPath(MembershipData.Url());

            string filename = GetSafeFilename(claimedIdentifier.OriginalString);

            return Path.Combine(directoryPath, filename);
        }

        public IEnumerable<Account> GetAdminAccounts()
        {
            if (_cacheService.AdminAccounts == null)
            {
                IEnumerable<Account> adminAccounts;

                var serializer = new XmlSerializer(typeof(Account[]));

                using (var reader = new StreamReader(_server.MapPath(MembershipData.AdminAccounts_xml)))
                {
                    adminAccounts = (IEnumerable<Account>) serializer.Deserialize(reader);
                }

                _cacheService.AdminAccounts = adminAccounts;
            }

            return _cacheService.AdminAccounts;
        }

        public void SaveAccount(Identifier claimedIdentifier, string emailAddress)
        {
            var account = new Account { ClaimedIdentifier = claimedIdentifier.OriginalString, EmailAddress = emailAddress, Name = CreateName(emailAddress) };

            var serializer = new XmlSerializer(typeof(Account));

            using (var writer = new StreamWriter(GetSavedAccountFilePath(claimedIdentifier)))
            {
                serializer.Serialize(writer, account);
            }
        }

        #endregion
    }
}