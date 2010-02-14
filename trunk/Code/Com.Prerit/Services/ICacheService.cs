using System.Collections.Generic;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface ICacheService
    {
        #region Methods

        Album GetAlbum(string dirPath);

        IEnumerable<Album> GetAlbums();

        Profile GetProfile(string id);

        Role GetRole(RoleType roleType);

        void SetAlbum(Album album, string dirPath);

        void SetAlbums(IEnumerable<Album> albums, string rootAlbumDirPath, string[] albumDirPaths);

        void SetProfile(Profile profile, string filePath);

        void SetRole(Role role, string filePath);

        #endregion
    }
}