using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public class CacheService : ICacheService
    {
        #region Constants

        private const string AlbumKeyBase = "Album-{0}";

        private const string AlbumsKey = "Albums";

        private const string ProfileKeyBase = "Profile-{0}";

        private const string RoleKeyBase = "Role-{0}";

        #endregion

        #region Fields

        private readonly Cache _cache;

        #endregion

        #region Constructors

        public CacheService(Cache cache)
        {
            if (cache == null)
            {
                throw new ArgumentNullException("cache");
            }

            _cache = cache;
        }

        #endregion

        #region Methods

        private string CreateAlbumKey(string dirPath)
        {
            return string.Format(AlbumKeyBase, dirPath);
        }

        private string CreateProfileKey(string id)
        {
            return string.Format(ProfileKeyBase, id);
        }

        private string CreateRoleKey(RoleType roleType)
        {
            return string.Format(RoleKeyBase, Enum.GetName(typeof(RoleType), roleType));
        }

        public Album GetAlbum(string dirPath)
        {
            if (dirPath == null)
            {
                throw new ArgumentNullException("dirPath");
            }

            return _cache[CreateAlbumKey(dirPath)] as Album;
        }

        public IEnumerable<Album> GetAlbums()
        {
            return _cache[AlbumsKey] as IEnumerable<Album>;
        }

        public Profile GetProfile(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            return _cache[CreateProfileKey(id)] as Profile;
        }

        public Role GetRole(RoleType roleType)
        {
            if (!Enum.IsDefined(typeof(RoleType), roleType))
            {
                throw new ArgumentOutOfRangeException("roleType");
            }

            return _cache[CreateRoleKey(roleType)] as Role;
        }

        public void SetAlbum(Album album, string dirPath)
        {
            if (album == null)
            {
                throw new ArgumentNullException("album");
            }

            if (dirPath == null)
            {
                throw new ArgumentNullException("dirPath");
            }

            _cache.Insert(CreateAlbumKey(dirPath), album, new CacheDependency(dirPath));
        }

        public void SetAlbums(IEnumerable<Album> albums, string rootAlbumDirPath, IEnumerable<string> albumDirPaths, IEnumerable<string> validAlbumDirPaths)
        {
            if (albums == null)
            {
                throw new ArgumentNullException("albums");
            }

            if (rootAlbumDirPath == null)
            {
                throw new ArgumentNullException("rootAlbumDirPath");
            }

            if (albumDirPaths == null)
            {
                throw new ArgumentNullException("albumDirPaths");
            }

            if (validAlbumDirPaths == null)
            {
                throw new ArgumentNullException("validAlbumDirPaths");
            }

            var diskDependencies = new List<string>
                                       {
                                           rootAlbumDirPath
                                       };

            diskDependencies.AddRange(albumDirPaths);

            string[] albumCachekeys = validAlbumDirPaths.Select(path => CreateAlbumKey(path)).ToArray();

            _cache.Insert(AlbumsKey, albums, new CacheDependency(diskDependencies.ToArray(), albumCachekeys));
        }

        public void SetProfile(Profile profile, string filePath)
        {
            if (profile == null)
            {
                throw new ArgumentNullException("profile");
            }

            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }

            _cache.Insert(CreateProfileKey(profile.Id), profile, new CacheDependency(filePath));
        }

        public void SetRole(Role role, string filePath)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            if (!Enum.IsDefined(typeof(RoleType), role.Type))
            {
                throw new ArgumentException("Role.Type is an undefined", "role");
            }

            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }

            _cache.Insert(CreateRoleKey(role.Type), role, new CacheDependency(filePath));
        }

        #endregion
    }
}