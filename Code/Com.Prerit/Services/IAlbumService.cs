using System.Collections.Generic;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IAlbumService
    {
        #region Methods

        Album GetAlbum(int year, string slug);

        WebImage GetAlbumPhoto(int year, string slug, int photoIndex, AlbumPhotoType albumPhotoType);

        WebImage GetAlbumPortrait(int year, string slug);

        IEnumerable<Album> GetAlbums();

        IEnumerable<Album> GetAlbums(int year);

        IEnumerable<string> GetAlbumSlugs(int year);

        IEnumerable<int> GetAlbumYears();

        #endregion
    }
}