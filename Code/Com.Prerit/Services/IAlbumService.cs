using System.Collections.Generic;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IAlbumService
    {
        #region Methods

        IEnumerable<Album> GetAlbums();

        IEnumerable<Album> GetAlbums(int year);

        IEnumerable<string> GetAlbumSlugs(int year);

        IEnumerable<int> GetAlbumYears();

        #endregion
    }
}