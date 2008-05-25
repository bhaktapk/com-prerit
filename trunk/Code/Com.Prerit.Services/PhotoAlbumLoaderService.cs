using System;

using Com.Prerit.Domain;
using Com.Prerit.Services.Caching;

namespace Com.Prerit.Services
{
    public class PhotoAlbumLoaderService : IPhotoAlbumLoaderService
    {
        #region Fields

        private readonly IAlbumYearLoaderService _albumYearLoaderService;

        private static readonly object _albumYearsSyncRoot = new object();

        #endregion

        #region Constructors

        public PhotoAlbumLoaderService(IAlbumYearLoaderService albumYearLoaderService)
        {
            if (albumYearLoaderService == null)
            {
                throw new ArgumentNullException("albumYearLoaderService");
            }

            _albumYearLoaderService = albumYearLoaderService;
        }

        #endregion

        #region Methods

        public AlbumYear[] Load()
        {
            AlbumYear[] result = TypedCache.AlbumYears;

            if (result == null)
            {
                lock (_albumYearsSyncRoot)
                {
                    result = TypedCache.AlbumYears;

                    if (result == null)
                    {
                        result = _albumYearLoaderService.Load();

                        TypedCache.AlbumYears = result;
                    }
                    else
                    {
                        result = TypedCache.AlbumYears;
                    }
                }
            }

            return result;
        }

        #endregion
    }
}