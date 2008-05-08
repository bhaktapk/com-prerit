using System.Collections.Generic;

namespace Com.Prerit.Web.Services
{
    public interface IPhotoAlbumService
    {
        #region Methods

        List<Photo> GetListOfPhotosByAlbumName();

        List<List<Album>> GetListsOfAlbumsOrderByAlbumYear();

        List<List<Album>> GetListsOfAlbumsOrderByAlbumYear(int value);

        #endregion
    }
}