using System;

using Com.Prerit.Domain;
using Com.Prerit.Services.Caching;

namespace Com.Prerit.Services
{
    public class PhotoAlbumLoaderService : IAsyncLoaderService<AlbumYear[]>
    {
        #region Fields

        private readonly IAlbumYearLoaderService _albumYearLoaderService;

        private readonly IAsyncCacheItemLoaderService _asyncCacheItemLoaderService;

        private static IAsyncResult _asyncResult;

        private static readonly object _loadingSyncRoot = new object();

        #endregion

        #region Properties

        public AlbumYear[] LoadedObject
        {
            get { return TypedCache.GetAlbumYearsCacheItem(); }
        }

        public LoaderAsyncServiceStatus Status
        {
            get
            {
                LoaderAsyncServiceStatus result;

                if (_asyncResult == null)
                {
                    result = LoaderAsyncServiceStatus.Idle;
                }
                else
                {
                    if (!_asyncResult.IsCompleted)
                    {
                        result = LoaderAsyncServiceStatus.Loading;
                    }
                    else
                    {
                        if (TypedCache.GetAlbumYearsCacheItem() == null)
                        {
                            result = LoaderAsyncServiceStatus.FailedLoad;
                        }
                        else
                        {
                            result = LoaderAsyncServiceStatus.Completed;
                        }
                    }
                }

                return result;
            }
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

        public void LoadAsync()
        {
            if (LoadedObject == null && Status != LoaderAsyncServiceStatus.Loading)
            {
                lock (_loadingSyncRoot)
                {
                    if (LoadedObject == null && Status != LoaderAsyncServiceStatus.Loading)
                    {
                        _asyncResult = _asyncCacheItemLoaderService.LoadAsync(_albumYearLoaderService,
                                                                              albumYears => TypedCache.SetAlbumYearsCacheItem(albumYears, _albumYearLoaderService.VirtualPath));
                    }
                }
            }
        }

        #endregion
    }
}