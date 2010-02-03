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

        private readonly ICacheService _cacheService;

        private readonly HttpServerUtilityBase _server;

        private readonly ISessionService _sessionService;

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

        private TDeserialized Deserialize<TSerialized, TDeserialized>(string filePath)
        {
            var serializer = new XmlSerializer(typeof(TSerialized));

            using (var reader = new StreamReader(filePath))
            {
                return (TDeserialized) serializer.Deserialize(reader);
            }
        }

        public Account GetAccount(Identifier claimedIdentifier)
        {
            if (_sessionService.Account == null)
            {
                _sessionService.Account = Deserialize<Account, Account>(GetSavedAccountFilePath(claimedIdentifier));
            }

            return _sessionService.Account;
        }

        public IEnumerable<Account> GetAdminAccounts()
        {
            if (_cacheService.GetAdminAccounts() == null)
            {
                string filePath = _server.MapPath(MembershipData.AdminAccounts_xml);

                _cacheService.SetAdminAccounts(Deserialize<Account[], IEnumerable<Account>>(filePath), filePath);
            }

            return _cacheService.GetAdminAccounts();
        }

        private string GetSafeFilename(Identifier claimedIdentifier)
        {
            char[] characters = claimedIdentifier.OriginalString.ToCharArray();

            List<char> safeFilename = characters.Select(c => !Path.GetInvalidFileNameChars().Contains(c) ? c : '-').ToList();

            safeFilename.AddRange(".xml".ToCharArray());

            return new string(safeFilename.ToArray());
        }

        private string GetSavedAccountFilePath(Identifier claimedIdentifier)
        {
            string directoryPath = _server.MapPath(MembershipData.Url());

            string filename = GetSafeFilename(claimedIdentifier.OriginalString);

            return Path.Combine(directoryPath, filename);
        }

        public void SaveAccount(Identifier claimedIdentifier, string emailAddress)
        {
            var account = new Account { ClaimedIdentifier = claimedIdentifier.OriginalString, EmailAddress = emailAddress, Name = CreateName(emailAddress) };

            Serialize(GetSavedAccountFilePath(claimedIdentifier), account);
        }

        private void Serialize<T>(string filePath, T obj)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, obj);
            }
        }

        #endregion
    }
}