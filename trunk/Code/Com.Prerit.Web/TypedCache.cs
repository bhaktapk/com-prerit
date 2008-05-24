using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace Com.Prerit.Web
{
    public static class TypedCache
    {
        #region Properties

        public static AlbumYear[] AlbumYears
        {
            get
            {
                CacheKey cacheKey = CacheKey.AlbumYears;

                Trace.TraceInformation("Getting {0} from cache", cacheKey);

                return GetClone(GetCacheItem<AlbumYear[]>(cacheKey));
            }
            set
            {
                if (TypedAppSettings.CacheAlbumPhotos)
                {
                    SetCacheItem(CacheKey.AlbumYears, value, GetAlbumYearsDependency(value));
                }
            }
        }

        #endregion

        #region Methods

        private static CacheDependency GetAlbumYearsDependency(AlbumYear[] albumYears)
        {
            CacheDependency result = null;

            if (albumYears != null)
            {
                List<string> folderDependencyList = new List<string>();

                const string photoAlbumsVirtualPath = "~/photo_albums/";

                string photoAlbumsPhysicalPath = HostingEnvironment.MapPath(photoAlbumsVirtualPath);

                folderDependencyList.Add(photoAlbumsPhysicalPath);

                foreach (AlbumYear albumYear in albumYears)
                {
                    folderDependencyList.Add(albumYear.VirtualPath);

                    Debug.Assert(albumYear.Albums != null);

                    foreach (Album album in albumYear.Albums)
                    {
                        folderDependencyList.Add(HostingEnvironment.MapPath(album.VirtualPath));
                    }
                }

                result = new CacheDependency(folderDependencyList.ToArray());
            }

            return result;
        }

        private static T GetCacheItem<T>(CacheKey cacheKey) where T : class
        {
            T result = null;

            object untypedCacheItem = HttpRuntime.Cache[CacheKey.AlbumYears];

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

        private static T GetClone<T>(T original) where T : class, ICloneable
        {
            T result = null;

            if (original != null)
            {
                result = (T) original.Clone();
            }

            return result;
        }

        private static void SetCacheItem<T>(CacheKey cacheKey, T cacheItem, CacheDependency dependency) where T : class
        {
            Debug.Assert(cacheKey != null);

            if (cacheItem != null)
            {
                if (dependency == null)
                {
                    HttpRuntime.Cache.Insert(cacheKey, cacheItem);
                }
                else
                {
                    HttpRuntime.Cache.Insert(cacheKey, cacheItem, dependency);
                }
            }
            else
            {
                HttpRuntime.Cache.Remove(cacheKey);
            }
        }

        #endregion
    }
}