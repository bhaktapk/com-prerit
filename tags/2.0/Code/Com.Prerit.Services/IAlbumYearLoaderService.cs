using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public interface IAlbumYearLoaderService : ILoaderService<AlbumYear[]>
    {
        #region Properties

        string VirtualPath { get; }

        #endregion
    }
}