namespace Com.Prerit.Web.Services
{
    public interface IPhotoAlbumLoaderService
    {
        #region Properties

        string PhysicalPath { get; }

        string VirtualPath { get; }

        #endregion

        #region Methods

        AlbumYear[] Load();

        #endregion
    }
}