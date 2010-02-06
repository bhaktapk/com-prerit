using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using Com.Prerit.Domain;

using DotNetOpenAuth.OpenId;

using Links;

namespace Com.Prerit.Services
{
    public class ProfileService : IProfileService
    {
        #region Fields

        private readonly ICacheService _cacheService;

        private readonly HttpServerUtilityBase _server;

        private readonly IXmlStoreService _xmlStoreService;

        private static readonly object AccountDictionarySyncRoot = new object();

        private static readonly Dictionary<Identifier, object> AccountSyncRoots = new Dictionary<Identifier, object>();

        #endregion

        #region Constructors

        public ProfileService(ICacheService cacheService, IXmlStoreService xmlStoreService, HttpServerUtilityBase server)
        {
            if (cacheService == null)
            {
                throw new ArgumentNullException("cacheService");
            }

            if (xmlStoreService == null)
            {
                throw new ArgumentNullException("xmlStoreService");
            }

            if (server == null)
            {
                throw new ArgumentNullException("server");
            }

            _cacheService = cacheService;
            _xmlStoreService = xmlStoreService;
            _server = server;
        }

        #endregion

        #region Methods

        private string CreateName(string emailAddress)
        {
            return emailAddress.Replace("@", " at ").Replace(".", " dot ");
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

                        _cacheService.SetAccount(_xmlStoreService.Load<Account>(filePath), accountId, filePath);
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
            var account = new Account
                              {
                                  Id = id.OriginalString,
                                  EmailAddress = emailAddress,
                                  Name = CreateName(emailAddress)
                              };

            lock (GetAccountSyncRoot(id))
            {
                _xmlStoreService.Save(GetSavedAccountFilePath(id), account);
            }
        }

        #endregion
    }
}