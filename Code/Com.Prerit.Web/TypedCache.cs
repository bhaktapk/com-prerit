using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;

namespace Com.Prerit.Web
{
    public static class TypedCache
    {
        #region Properties

        public static SortedList<int, Album[]> AlbumsGroupedByAlbumYear
        {
            get { return GetCacheItem<SortedList<int, Album[]>>(CacheKey.AlbumsGroupedByAlbumYear); }
            set
            {
                if (TypedAppSettings.CacheAlbumPhotos)
                {
                    SetCacheItem(CacheKey.AlbumsGroupedByAlbumYear, value);
                }
            }
        }

        #endregion

        #region Methods

        private static T GetCacheItem<T>(CacheKey cacheKey) where T : class
        {
            T result = null;

            HttpContext context = GetContext();

            object untypedCacheItem = context.Cache[CacheKey.AlbumsGroupedByAlbumYear];

            if (untypedCacheItem != null)
            {
                T typedCacheItem = untypedCacheItem as T;

                if (typedCacheItem != null)
                {
                    result = typedCacheItem;
                }
                else
                {
                    Trace.TraceWarning(
                        string.Format("Cache key {0} contains an incorrect type {1} instead of {2}",
                                      cacheKey,
                                      untypedCacheItem.GetType().FullName,
                                      typeof(T).FullName));
                }
            }

            return result;
        }

        private static HttpContext GetContext()
        {
            HttpContext context = HttpContext.Current;

            if (context == null)
            {
                throw new Exception("Unable to obtain the Cache object because an HttpContext does not exist");
            }

            return context;
        }

        private static void SetCacheItem<T>(CacheKey cacheKey, T cacheItem) where T : class
        {
            Debug.Assert(cacheKey != null);

            HttpContext context = GetContext();

            if (cacheItem != null)
            {
                context.Cache[cacheKey] = cacheItem;
            }
            else
            {
                context.Cache.Remove(cacheKey);
            }
        }

        #endregion
    }
}