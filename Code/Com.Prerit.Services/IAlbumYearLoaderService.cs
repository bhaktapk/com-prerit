using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IAlbumYearLoaderService
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