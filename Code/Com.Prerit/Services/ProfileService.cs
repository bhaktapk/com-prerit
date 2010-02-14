using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Com.Prerit.Domain;

using Links;

namespace Com.Prerit.Services
{
    public class ProfileService : IProfileService
    {
        #region Fields

        private readonly ICacheService _cacheService;

        private readonly IDiskInputOutputService _diskInputOutputService;

        private static readonly object ProfileDictionarySyncRoot = new object();

        private static readonly Dictionary<string, object> ProfileSyncRoots = new Dictionary<string, object>();

        #endregion

        #region Constructors

        public ProfileService(ICacheService cacheService, IDiskInputOutputService diskInputOutputService)
        {
            if (cacheService == null)
            {
                throw new ArgumentNullException("cacheService");
            }

            if (diskInputOutputService == null)
            {
                throw new ArgumentNullException("diskInputOutputService");
            }

            _cacheService = cacheService;
            _diskInputOutputService = diskInputOutputService;
        }

        #endregion

        #region Methods

        private string CreateName(string emailAddress)
        {
            return emailAddress.Replace("@", " at ").Replace(".", " dot ");
        }

        private string GetFilePath(string id)
        {
            string directoryPath = _diskInputOutputService.MapPath(App_Data.Profiles.Url());

            string filename = GetSafeFilename(id);

            return Path.Combine(directoryPath, filename);
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

                        if (File.Exists(filePath))
                        {
                            _cacheService.SetProfile(_diskInputOutputService.LoadXmlFile<Profile>(filePath), filePath);
                        }
                    }
                }
            }

            return _cacheService.GetProfile(id);
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

        private string GetSafeFilename(string id)
        {
            char[] characters = id.ToCharArray();

            List<char> safeFilename = characters.Select(c => !Path.GetInvalidFileNameChars().Contains(c) ? c : '-').ToList();

            safeFilename.AddRange(".xml".ToCharArray());

            return new string(safeFilename.ToArray());
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
                string filePath = GetFilePath(id);

                _diskInputOutputService.SaveXmlFile(filePath, profile);

                _cacheService.SetProfile(profile, filePath);
            }
        }

        #endregion
    }
}