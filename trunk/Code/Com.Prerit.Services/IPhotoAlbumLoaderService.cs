using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IPhotoAlbumLoaderService
    {
        #region Methods

        AlbumYear[] Load();

        #endregion
    }
}