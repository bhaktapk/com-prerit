using System.Collections.Generic;

namespace Com.Prerit.Web.Services
{
    public interface IPhotoAlbumService
    {
        #region Methods

        Photo[] GetPhotosByAlbumYearAndAlbumName(int albumYear, string albumName);

        SortedList<int, Album[]> GetAlbumsGroupedByAlbumYear();

        SortedList<int, Album[]> GetAlbumsByAlbumYearGroupedByAlbumYear(int albumYear);

        #endregion
    }
}