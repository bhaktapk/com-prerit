using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace Com.Prerit.Web.Caching
{
    public static class CacheItemDependency
    {
        #region Methods

        public static CacheDependency GetAlbumYearsDependency(AlbumYear[] albumYears, string photoAlbumsVirtualPath)
        {
            if (albumYears == null)
            {
                throw new ArgumentNullException("albumYears");
            }

            if (!VirtualPathUtility.IsAbsolute(photoAlbumsVirtualPath) && !VirtualPathUtility.IsAppRelative(photoAlbumsVirtualPath))
            {
                throw new ArgumentException("Path must either be an absolute virtual path or app relative virtual path", "photoAlbumsVirtualPath");
            }

            CacheDependency result;

            List<string> folderDependencyList = new List<string>();

            folderDependencyList.Add(HostingEnvironment.MapPath(photoAlbumsVirtualPath));

            foreach (AlbumYear albumYear in albumYears)
            {
                folderDependencyList.Add(HostingEnvironment.MapPath(albumYear.VirtualPath));

                Debug.Assert(albumYear.Albums != null);

                foreach (Album album in albumYear.Albums)
                {
                    folderDependencyList.Add(HostingEnvironment.MapPath(album.VirtualPath));
                }
            }

            result = new CacheDependency(folderDependencyList.ToArray());

            return result;
        }

        #endregion
    }
}