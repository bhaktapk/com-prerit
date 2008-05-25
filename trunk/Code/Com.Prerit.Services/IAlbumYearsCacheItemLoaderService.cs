using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IAlbumYearsCacheItemLoaderService
    {
        #region Methods

        AlbumYear[] Load();

        #endregion
    }
}