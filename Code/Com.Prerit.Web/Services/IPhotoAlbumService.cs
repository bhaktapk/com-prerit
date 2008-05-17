using System.Collections.Generic;

namespace Com.Prerit.Web.Services
{
    // TODO: change method names from "get" to "find" and return null if not found
    public interface IPhotoAlbumService
    {
        #region Methods

        SortedList<int, Album[]> GetAlbumsByAlbumYearGroupedByAlbumYear(int albumYear);

        SortedList<int, Album[]> GetAlbumsGroupedByAlbumYear();

        Photo[] GetPhotosByAlbumYearAndAlbumName(int albumYear, string albumName);

        #endregion
    }
}