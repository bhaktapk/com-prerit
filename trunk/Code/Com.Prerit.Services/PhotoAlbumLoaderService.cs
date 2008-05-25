using System;

using Com.Prerit.Domain;
using Com.Prerit.Services.Caching;

namespace Com.Prerit.Services
{
    public class PhotoAlbumLoaderService : IPhotoAlbumLoaderService
    {
        #region Fields

        private readonly IAlbumYearLoaderService _albumYearLoaderService;

        private readonly IAsyncCacheItemLoaderService _asyncCacheItemLoaderService;

        private static IAsyncResult _asyncResult;

        private static readonly object _loadingSyncRoot = new object();

        #endregion

        #region Properties

        public AlbumYear[] GetLoadedObject()
        {
            return TypedCache.AlbumYears;
        }

        #endregion

        #region Constructors

        public PhotoAlbumLoaderService(IAlbumYearLoaderService albumYearLoaderService, IAsyncCacheItemLoaderService asyncCacheItemLoaderService)
        {
            if (albumYearLoaderService == null)
            {
                throw new ArgumentNullException("albumYearLoaderService");
            }

            if (asyncCacheItemLoaderService == null)
            {
                throw new ArgumentNullException("asyncCacheItemLoaderService");
            }

            _albumYearLoaderService = albumYearLoaderService;
            _asyncCacheItemLoaderService = asyncCacheItemLoaderService;
        }

        #endregion

        #region Methods

        public bool IsLoading()
        {
            bool result = false;

            if (_asyncResult != null && !_asyncResult.IsCompleted)
            {
                result = true;
            }

            return result;
        }

        public void LoadAsync()
        {
            AlbumYear[] cacheItem = TypedCache.AlbumYears;

            if (cacheItem == null && !IsLoading())
            {
                lock (_loadingSyncRoot)
                {
                    cacheItem = TypedCache.AlbumYears;

                    if (cacheItem == null && !IsLoading())
                    {
                        _asyncResult = _asyncCacheItemLoaderService.LoadAsync(_albumYearLoaderService, albumYears => TypedCache.AlbumYears = albumYears);
                    }
                    else
                    {
                        cacheItem = TypedCache.AlbumYears;
                    }
                }
            }
        }

        #endregion
    }
}