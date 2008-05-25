﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

using Com.Prerit.Domain;

namespace Com.Prerit.Services.Caching
{
    public static class CacheItemDependency
    {
        #region Methods

        public static CacheDependency GetAlbumYearsDependency(AlbumYear[] albumYears, string albumYearsVirtualPath)
        {
            if (albumYears == null)
            {
                throw new ArgumentNullException("albumYears");
            }

            if (albumYearsVirtualPath == null)
            {
                throw new ArgumentNullException("albumYearsVirtualPath");
            }

            if (!VirtualPathUtility.IsAbsolute(albumYearsVirtualPath) && !VirtualPathUtility.IsAppRelative(albumYearsVirtualPath))
            {
                throw new ArgumentException("Path must either be an absolute virtual path or app relative virtual path", "albumYearsVirtualPath");
            }

            CacheDependency result;

            List<string> folderDependencyList = new List<string>();

            folderDependencyList.Add(HostingEnvironment.MapPath(albumYearsVirtualPath));

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