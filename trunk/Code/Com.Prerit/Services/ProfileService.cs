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

        private static readonly object ProfileDictionarySyncRoot = new object();

        private static readonly Dictionary<string, object> ProfileSyncRoots = new Dictionary<string, object>();

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

        private object GetProfileSyncRoot(string id)
        {
            if (!ProfileSyncRoots.ContainsKey(id))
            {
                lock (ProfileDictionarySyncRoot)
                {
                    if (!ProfileSyncRoots.ContainsKey(id))
                    {
                        ProfileSyncRoots.Add(id, new object());
                    }
                }
            }

            return ProfileSyncRoots[id];
        }

        public Profile GetProfile(string id)
        {
            if (_cacheService.GetProfile(id) == null)
            {
                lock (GetProfileSyncRoot(id))
                {
                    if (_cacheService.GetProfile(id) == null)
                    {
                        string filePath = GetFilePath(id);

                        _cacheService.SetProfile(_xmlStoreService.Load<Profile>(filePath), filePath);
                    }
                }
            }

            return _cacheService.GetProfile(id);
        }

        private string GetSafeFilename(string id)
        {
            char[] characters = id.ToCharArray();

            List<char> safeFilename = characters.Select(c => !Path.GetInvalidFileNameChars().Contains(c) ? c : '-').ToList();

            safeFilename.AddRange(".xml".ToCharArray());

            return new string(safeFilename.ToArray());
        }

        private string GetFilePath(string id)
        {
            string directoryPath = _server.MapPath(App_Data.Profiles.Url());

            string filename = GetSafeFilename(id);

            return Path.Combine(directoryPath, filename);
        }

        public void SaveProfile(string id, string emailAddress)
        {
            var profile = new Profile
                              {
                                  Id = id,
                                  EmailAddress = emailAddress,
                                  Name = CreateName(emailAddress)
                              };

            lock (GetProfileSyncRoot(id))
            {
                _xmlStoreService.Save(GetFilePath(id), profile);
            }
        }

        #endregion
    }
}