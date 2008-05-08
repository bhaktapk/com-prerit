using System.Collections.Generic;

namespace Com.Prerit.Web.Services
{
    public interface IPhotoAlbumService
    {
        #region Methods

        List<Photo> GetPhotosByAlbumName(string albumName);

        List<List<Album>> GetAlbumsGroupedByAlbumYear();

        List<List<Album>> GetAlbumsByAlbumYearGroupedByAlbumYear(int albumYear);

        #endregion
    }
}