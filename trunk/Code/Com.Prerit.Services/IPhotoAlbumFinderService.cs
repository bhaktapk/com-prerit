using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IPhotoAlbumFinderService
    {
        #region Methods

        AlbumYear FindAlbumYear(int year);

        AlbumYear[] FindAlbumYears();

        Photo[] FindPhotos(int year, string albumName);

        #endregion
    }
}