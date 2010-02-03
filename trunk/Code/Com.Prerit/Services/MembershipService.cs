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

        private static readonly object AccountDictionarySyncRoot = new object();

        private static readonly Dictionary<Identifier, object> AccountSyncRoots = new Dictionary<Identifier, object>();

        private static readonly object AdminAccountsSyncRoot = new object();

        #endregion

        #region Constructors

        public MembershipService(ICacheService cacheService, HttpServerUtilityBase server)
        {
            if (cacheService == null)
            {
                throw new ArgumentNullException("cacheService");
            }

            if (server == null)
            {
                throw new ArgumentNullException("server");
            }

            _cacheService = cacheService;
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

        public Account GetAccount(Identifier id)
        {
            string accountId = id.OriginalString;

            if (_cacheService.GetAccount(accountId) == null)
            {
                lock (GetAccountSyncRoot(accountId))
                {
                    if (_cacheService.GetAccount(accountId) == null)
                    {
                        string filePath = GetSavedAccountFilePath(id);

                        _cacheService.SetAccount(Deserialize<Account, Account>(filePath), accountId, filePath);
                    }
                }
            }

            return _cacheService.GetAccount(accountId);
        }

        private object GetAccountSyncRoot(Identifier id)
        {
            if (!AccountSyncRoots.ContainsKey(id))
            {
                lock (AccountDictionarySyncRoot)
                {
                    if (!AccountSyncRoots.ContainsKey(id))
                    {
                        AccountSyncRoots.Add(id, new object());
                    }
                }
            }

            return AccountSyncRoots[id];
        }

        public IEnumerable<Account> GetAdminAccounts()
        {
            if (_cacheService.GetAdminAccounts() == null)
            {
                lock (AdminAccountsSyncRoot)
                {
                    if (_cacheService.GetAdminAccounts() == null)
                    {
                        string filePath = _server.MapPath(MembershipData.AdminAccounts_xml);

                        _cacheService.SetAdminAccounts(Deserialize<Account[], IEnumerable<Account>>(filePath), filePath);
                    }
                }
            }

            return _cacheService.GetAdminAccounts();
        }

        private string GetSafeFilename(Identifier id)
        {
            char[] characters = id.OriginalString.ToCharArray();

            List<char> safeFilename = characters.Select(c => !Path.GetInvalidFileNameChars().Contains(c) ? c : '-').ToList();

            safeFilename.AddRange(".xml".ToCharArray());

            return new string(safeFilename.ToArray());
        }

        private string GetSavedAccountFilePath(Identifier id)
        {
            string directoryPath = _server.MapPath(MembershipData.Url());

            string filename = GetSafeFilename(id.OriginalString);

            return Path.Combine(directoryPath, filename);
        }

        public void SaveAccount(Identifier id, string emailAddress)
        {
            var account = new Account { Id = id.OriginalString, EmailAddress = emailAddress, Name = CreateName(emailAddress) };

            lock (GetAccountSyncRoot(id))
            {
                Serialize(GetSavedAccountFilePath(id), account);
            }
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