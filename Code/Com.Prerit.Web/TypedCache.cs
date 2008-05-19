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

        public static SortedList<int, Album[]> AlbumsGroupedByAlbumYear
        {
            get
            {
                CacheKey cacheKey = CacheKey.AlbumsGroupedByAlbumYear;

                Trace.TraceInformation("Getting {0} from cache", cacheKey);

                return GetCopy(GetCacheItem<SortedList<int, Album[]>>(cacheKey));
            }
            set
            {
                if (TypedAppSettings.CacheAlbumPhotos)
                {
                    SetCacheItem(CacheKey.AlbumsGroupedByAlbumYear, value, GetAlbumsGroupedByAlbumYearDependency(value));
                }
            }
        }

        #endregion

        #region Methods

        private static CacheDependency GetAlbumsGroupedByAlbumYearDependency(SortedList<int, Album[]> albumsGroupedByAlbumYear)
        {
            CacheDependency result = null;

            if (albumsGroupedByAlbumYear != null)
            {
                List<string> folderDependencyList = new List<string>();

                const string photoAlbumsVirtualPath = "~/photo_albums/";

                string photoAlbumsPhysicalPath = HostingEnvironment.MapPath(photoAlbumsVirtualPath);

                folderDependencyList.Add(photoAlbumsPhysicalPath);

                foreach (KeyValuePair<int, Album[]> keyValuePair in albumsGroupedByAlbumYear)
                {
                    string albumYearPhysicalPath = HostingEnvironment.MapPath(Path.Combine(photoAlbumsVirtualPath, keyValuePair.Key.ToString()));

                    folderDependencyList.Add(albumYearPhysicalPath);

                    Debug.Assert(keyValuePair.Value != null);

                    foreach (Album album in keyValuePair.Value)
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

        private static SortedList<int, Album[]> GetCopy(SortedList<int, Album[]> result)
        {
            if (result != null)
            {
                result = new SortedList<int, Album[]>(result);
            }

            return result;
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