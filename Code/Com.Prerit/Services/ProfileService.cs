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

        private object AddProfileSyncRoot(string id)
        {
            var profileSyncRoot = new object();

            ProfileSyncRoots.Add(id, profileSyncRoot);

            return profileSyncRoot;
        }

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
            Profile profile = _cacheService.GetProfile(id);

            if (profile != null)
            {
                return profile;
            }

            lock (GetProfileSyncRoot(id))
            {
                return _cacheService.GetProfile(id) ?? TryLoadProfile(id);
            }
        }

        private object GetProfileSyncRoot(string id)
        {
            object profileSyncRoot;

            if (ProfileSyncRoots.TryGetValue(id, out profileSyncRoot))
            {
                return profileSyncRoot;
            }

            lock (ProfileDictionarySyncRoot)
            {
                if (ProfileSyncRoots.TryGetValue(id, out profileSyncRoot))
                {
                    return profileSyncRoot;
                }

                return AddProfileSyncRoot(id);
            }
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
            lock (GetProfileSyncRoot(id))
            {
                var profile = new Profile
                                  {
                                      Id = id,
                                      EmailAddress = emailAddress,
                                      Name = CreateName(emailAddress)
                                  };

                string filePath = GetFilePath(id);

                _diskInputOutputService.SaveXmlFile(filePath, profile);

                _cacheService.SetProfile(profile, filePath);
            }
        }

        private Profile TryLoadProfile(string id)
        {
            string filePath = GetFilePath(id);

            if (!_diskInputOutputService.FileExists(filePath))
            {
                return null;
            }

            var profile = _diskInputOutputService.LoadXmlFile<Profile>(filePath);

            _cacheService.SetProfile(profile, filePath);

            return profile;
        }

        #endregion
    }
}