using System;
using System.Diagnostics;
using System.Web;
using System.Web.Caching;

using Com.Prerit.Configuration;
using Com.Prerit.Domain;

namespace Com.Prerit.Services.Caching
{
    public static class TypedCache
    {
        #region Properties

        public static AlbumYear[] AlbumYears
        {
            get { return GetClone(GetCacheItem<AlbumYear[]>(CacheKey.AlbumYears)); }
            set
            {
                if (TypedAppSettings.CacheAlbumPhotos)
                {
                    CacheDependency cacheDependency = null;

                    if (value != null)
                    {
                        // TODO: make "~/photo_albums/" an app setting or const
                        cacheDependency = CacheItemDependency.GetAlbumYearsDependency(value, "~/photo_albums/");
                    }

                    SetCacheItem(CacheKey.AlbumYears, value, cacheDependency);
                }
            }
        }

        #endregion

        #region Methods

        private static T GetCacheItem<T>(CacheKey cacheKey) where T : class
        {
            T result = null;

            object untypedCacheItem = HttpRuntime.Cache[CacheKey.AlbumYears];

            if (untypedCacheItem != null)
            {
                var typedCacheItem = untypedCacheItem as T;

                if (typedCacheItem != null)
                {
                    Trace.TraceInformation("Getting {0} from cache (cache item was not null)", cacheKey);

                    result = typedCacheItem;
                }
                else
                {
                    Trace.TraceWarning(string.Format("Removing {0} from cache because it contains the type {1} instead of {2}",
                                                     cacheKey,
                                                     untypedCacheItem.GetType().FullName,
                                                     typeof(T).FullName));

                    HttpRuntime.Cache.Remove(cacheKey);
                }
            }
            else
            {
                Trace.TraceInformation("Getting {0} from cache (cache item was null)", cacheKey);
            }

            return result;
        }

        private static T GetClone<T>(T original) where T : class, ICloneable
        {
            T result = null;

            if (original != null)
            {
                result = (T) original.Clone();
            }

            return result;
        }

        private static void ItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            Trace.TraceInformation(string.Format("Cache item {0} was removed for the following reason: {1}", key, reason));
        }

        private static void SetCacheItem<T>(CacheKey cacheKey, T cacheItem, CacheDependency dependency) where T : class
        {
            Debug.Assert(cacheKey != null);
            Debug.Assert(!(cacheItem == null && dependency != null));

            if (cacheItem != null)
            {
                if (dependency == null)
                {
                    Trace.TraceInformation("Inserting {0} into cache", cacheKey);
                }
                else
                {
                    Trace.TraceInformation("Inserting {0} into cache with a dependency", cacheKey);
                }

                HttpRuntime.Cache.Insert(cacheKey,
                                         cacheItem,
                                         dependency,
                                         Cache.NoAbsoluteExpiration,
                                         Cache.NoSlidingExpiration,
                                         CacheItemPriority.Default,
                                         ItemRemovedCallback);
            }
            else
            {
                Trace.TraceInformation("Removing {0} from cache", cacheKey);

                HttpRuntime.Cache.Remove(cacheKey);
            }
        }

        #endregion
    }
}