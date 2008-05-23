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
                    string albumYearPhysicalPath = HostingEnvironment.MapPath(Path.Combine(photoAlbumsVirtualPath, albumYear.Year.ToString()));

                    folderDependencyList.Add(albumYearPhysicalPath);

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

            HttpContext context = GetContext();

            object untypedCacheItem = context.Cache[CacheKey.AlbumYears];

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

        private static HttpContext GetContext()
        {
            HttpContext context = HttpContext.Current;

            if (context == null)
            {
                throw new Exception("Unable to obtain the Cache object because an HttpContext does not exist");
            }

            return context;
        }

        private static void SetCacheItem<T>(CacheKey cacheKey, T cacheItem, CacheDependency dependency) where T : class
        {
            Debug.Assert(cacheKey != null);

            HttpContext context = GetContext();

            if (cacheItem != null)
            {
                if (dependency == null)
                {
                    context.Cache.Insert(cacheKey, cacheItem);
                }
                else
                {
                    context.Cache.Insert(cacheKey, cacheItem, dependency);
                }
            }
            else
            {
                context.Cache.Remove(cacheKey);
            }
        }

        #endregion
    }
}