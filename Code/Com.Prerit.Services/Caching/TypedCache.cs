using System;
using System.Diagnostics;
using System.Web;
using System.Web.Caching;

using Com.Prerit.Core.Configuration;
using Com.Prerit.Domain;

namespace Com.Prerit.Services.Caching
{
    public static class TypedCache
    {
        #region Methods

        public static AlbumYear[] GetAlbumYearsCacheItem()
        {
            return GetClone(GetCacheItem<AlbumYear[]>(CacheKey.AlbumYears));
        }

        public static T GetCacheItem<T>(CacheKey cacheKey) where T : class
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

        public static void SetAlbumYearsCacheItem(AlbumYear[] albumYears, string albumYearsVirtualPath)
        {
            if (TypedAppSettings.CacheAlbumPhotos)
            {
                CacheDependency cacheDependency = null;

                if (albumYears != null)
                {
                    if (albumYearsVirtualPath == null)
                    {
                        throw new ArgumentNullException("albumYearsVirtualPath");
                    }

                    if (!VirtualPathUtility.IsAbsolute(albumYearsVirtualPath) && !VirtualPathUtility.IsAppRelative(albumYearsVirtualPath))
                    {
                        throw new ArgumentException("Path must either be an absolute virtual path or app relative virtual path", "albumYearsVirtualPath");
                    }

                    cacheDependency = CacheItemDependency.GetAlbumYearsDependency(albumYears, albumYearsVirtualPath);
                }

                SetCacheItem(CacheKey.AlbumYears, albumYears, cacheDependency);
            }
        }

        public static void SetCacheItem<T>(CacheKey cacheKey, T cacheItem, CacheDependency dependency) where T : class
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