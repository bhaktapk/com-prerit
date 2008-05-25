using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IPhotoAlbumLoaderService
    {
        #region Methods

        AlbumYear[] GetLoadedObject();

        bool IsLoading();

        void LoadAsync();

        #endregion
    }
}