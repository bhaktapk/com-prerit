namespace Com.Prerit.Web.Services
{
    public interface IPhotoAlbumService
    {
        #region Methods

        AlbumYear FindAlbumYear(int year);

        AlbumYear[] FindAlbumYears();

        Photo[] FindPhotos(int year, string albumName);

        #endregion
    }
}