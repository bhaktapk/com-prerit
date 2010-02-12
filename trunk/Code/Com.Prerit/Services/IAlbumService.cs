using System.Collections.Generic;
using System.Linq;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IAlbumService
    {
        #region Methods

        IEnumerable<Album> GetAlbums();

        IEnumerable<Album> GetAlbums(int year);

        IEnumerable<IGrouping<int, Album>> GetAlbumsGroupedByYear();

        IEnumerable<string> GetAlbumTitles(int year);

        IEnumerable<int> GetAlbumYears();

        #endregion
    }
}