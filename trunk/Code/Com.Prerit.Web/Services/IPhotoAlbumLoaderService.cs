using System.Collections.Generic;

namespace Com.Prerit.Web.Services
{
    public interface IPhotoAlbumLoaderService
    {
        #region Properties

        string VirtualPath { get; }

        #endregion

        #region Methods

        SortedList<int, Album[]> Load();

        #endregion
    }
}