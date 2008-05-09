using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace Com.Prerit.Web.Services
{
    public class PhotoAlbumLoaderService : IPhotoAlbumLoaderService
    {
        #region Fields

        private readonly string _physicalPath;

        #endregion

        #region Properties

        public string VirtualPath
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public PhotoAlbumLoaderService(string virtualPath)
        {
            if (!VirtualPathUtility.IsAbsolute(virtualPath) && !VirtualPathUtility.IsAppRelative(virtualPath))
            {
                throw new ArgumentException("Path must either be an absolute virtual path or app relative virtual path", "virtualPath");
            }

            VirtualPath = virtualPath;

            _physicalPath = HostingEnvironment.MapPath(virtualPath);
        }

        #endregion

        #region Methods

        private Album[] GetAlbums(DirectoryInfo albumYearDirectoryInfo, int albumYear, string albumYearVirtualPath)
        {
            List<Album> result = new List<Album>();

            foreach (DirectoryInfo albumDirectoryInfo in albumYearDirectoryInfo.GetDirectories())
            {
                string albumVirtualPath = VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.Combine(albumYearVirtualPath, albumDirectoryInfo.Name));

                Photo[] photos = GetPhotos(albumDirectoryInfo, albumVirtualPath);

                if (photos.Length != 0)
                {
                    result.Add(new Album(albumDirectoryInfo.Name, albumYear, albumVirtualPath, photos[0], photos));
                }
                else
                {
                    Trace.TraceWarning(string.Format("Directory {0} contained no useable photos", albumDirectoryInfo));
                }
            }

            return result.ToArray();
        }

        private SortedList<int, Album[]> GetAlbumsGroupByAlbumYears()
        {
            SortedList<int, Album[]> result = new SortedList<int, Album[]>();

            DirectoryInfo parentDirectoryInfo = new DirectoryInfo(_physicalPath);

            foreach (DirectoryInfo albumYearDirectoryInfo in parentDirectoryInfo.GetDirectories())
            {
                int parsedAlbumYear;

                if (int.TryParse(albumYearDirectoryInfo.Name, out parsedAlbumYear))
                {
                    // TODO: make minAlbumYear configurable
                    const int minAlbumYear = 1979;
                    int maxAlbumYear = DateTime.Today.Year;

                    if (parsedAlbumYear >= minAlbumYear && parsedAlbumYear <= maxAlbumYear)
                    {
                        string albumYearVirtualPath =
                            VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.Combine(VirtualPath, albumYearDirectoryInfo.Name));

                        result[parsedAlbumYear] = GetAlbums(albumYearDirectoryInfo, parsedAlbumYear, albumYearVirtualPath);
                    }
                    else
                    {
                        Trace.TraceWarning(
                            string.Format("Directory name {0} does not fall between {1} and {2}", albumYearDirectoryInfo.Name, minAlbumYear, maxAlbumYear));
                    }
                }
                else
                {
                    Trace.TraceWarning(string.Format("Non-numeric directory name {0} was found in {1}", albumYearDirectoryInfo.Name, VirtualPath));
                }
            }

            return result;
        }

        private Photo[] GetPhotos(DirectoryInfo albumDirectoryInfo, string albumVirtualPath)
        {
            List<Photo> result = new List<Photo>();

            // TODO: make allowablePhotoExtension configurable
            const string allowablePhotoExtension = "*.jpg";

            foreach (FileInfo photoFileInfo in albumDirectoryInfo.GetFiles(allowablePhotoExtension))
            {
                try
                {
                    using (FileStream photoFileStream = File.OpenRead(photoFileInfo.FullName))
                    {
                        using (Image image = Image.FromStream(photoFileStream))
                        {
                            result.Add(
                                new Photo(photoFileInfo.Name, VirtualPathUtility.Combine(albumVirtualPath, photoFileInfo.Name), image.Height, image.Width));
                        }
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.ToString());
                }
            }

            return result.ToArray();
        }

        public SortedList<int, Album[]> Load()
        {
            return GetAlbumsGroupByAlbumYears();
        }

        #endregion
    }
}