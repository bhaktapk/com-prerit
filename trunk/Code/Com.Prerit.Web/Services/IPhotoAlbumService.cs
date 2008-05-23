using System.Collections.Generic;

namespace Com.Prerit.Web.Services
{
    public interface IPhotoAlbumService
    {
        #region Methods

        SortedList<int, Album[]> FindAlbums();

        SortedList<int, Album[]> FindAlbums(int albumYear);

        Photo[] FindPhotos(int albumYear, string albumName);

        #endregion
    }
}