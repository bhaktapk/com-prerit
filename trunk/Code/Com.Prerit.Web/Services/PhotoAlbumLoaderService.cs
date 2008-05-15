using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace Com.Prerit.Web.Services
{
    public class PhotoAlbumLoaderService : IPhotoAlbumLoaderService
    {
        #region Constants

        // TODO: make _albumCoverFileName configurable
        private const string _albumCoverFileName = "album_cover.jpg";

        // TODO: make _allowablePhotoExtension configurable
        private const string _allowablePhotoExtension = "*.jpg";

        #endregion

        #region Fields

        private static readonly object _albumsGroupedByAlbumYearSyncRoot = new object();

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

        private WebImage CreateAlbumCover(string albumVirtualPath, string albumCoverFileName, string albumCoverPhysicalPath, Photo photo)
        {
            WebImage result;

            using (FileStream photoFileStream = File.OpenRead(HostingEnvironment.MapPath(photo.VirtualPath)))
            {
                using (Image photoImage = Image.FromStream(photoFileStream))
                {
                    Image albumCoverImage;

                    const int maxAlbumCoverDimension = 240;

                    int height;
                    int width;

                    string albumCoverVirtualPath = VirtualPathUtility.Combine(albumVirtualPath, albumCoverFileName);

                    ResizeHeightAndWidth(photoImage, maxAlbumCoverDimension, out height, out width);

                    try
                    {
                        albumCoverImage = photoImage.GetThumbnailImage(width,
                                                                       height,
                                                                       delegate
                                                                       {
                                                                           return false;
                                                                       },
                                                                       IntPtr.Zero);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Album cover could not be created for photo {0}", albumCoverPhysicalPath), e);
                    }

                    try
                    {
                        albumCoverImage.Save(albumCoverPhysicalPath, ImageFormat.Jpeg);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Album cover could not be saved for photo {0}", albumCoverPhysicalPath), e);
                    }

                    result = new WebImage(photo.Caption, albumCoverVirtualPath, albumCoverImage.Height, albumCoverImage.Width);
                }
            }

            return result;
        }

        private WebImage GetAlbumCover(string albumVirtualPath, Photo photo)
        {
            WebImage result;

            string albumCoverPhysicalPath = Path.Combine(HostingEnvironment.MapPath(albumVirtualPath), _albumCoverFileName);

            if (File.Exists(albumCoverPhysicalPath))
            {
                using (FileStream albumCoverFileStream = File.OpenRead(albumCoverPhysicalPath))
                {
                    using (Image albumCoverImage = Image.FromStream(albumCoverFileStream))
                    {
                        // TODO: check size and resize and log if necessary

                        result =
                            new WebImage(_albumCoverFileName,
                                         VirtualPathUtility.Combine(albumVirtualPath, _albumCoverFileName),
                                         albumCoverImage.Height,
                                         albumCoverImage.Width);
                    }
                }
            }
            else
            {
                // TODO: create cover album from first photo

                result = CreateAlbumCover(albumVirtualPath, _albumCoverFileName, albumCoverPhysicalPath, photo);
            }

            return result;
        }

        private Album[] GetAlbums(DirectoryInfo albumYearDirectoryInfo, int albumYear, string albumYearVirtualPath)
        {
            List<Album> result = new List<Album>();

            foreach (DirectoryInfo albumDirectoryInfo in albumYearDirectoryInfo.GetDirectories())
            {
                string albumVirtualPath = VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.Combine(albumYearVirtualPath, albumDirectoryInfo.Name));

                Photo[] photos = GetPhotos(albumDirectoryInfo, albumVirtualPath);

                if (photos.Length != 0)
                {
                    WebImage albumCover = GetAlbumCover(albumVirtualPath, photos[0]);

                    result.Add(new Album(albumDirectoryInfo.Name, albumYear, albumVirtualPath, albumCover, photos));
                }
                else
                {
                    Trace.TraceWarning(string.Format("Directory {0} contained no useable photos", albumDirectoryInfo.FullName));
                }
            }

            return result.ToArray();
        }

        private SortedList<int, Album[]> GetAlbumsGroupedByAlbumYears()
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
                            string.Format("Directory name {0} does not fall between {1} and {2}", albumYearDirectoryInfo.FullName, minAlbumYear, maxAlbumYear));
                    }
                }
                else
                {
                    Trace.TraceWarning(string.Format("Non-numeric directory name {0} was found in {1}", albumYearDirectoryInfo.FullName, VirtualPath));
                }
            }

            return result;
        }

        private WebImage GetAssociatedThumbnail(DirectoryInfo albumDirectoryInfo, FileInfo photoFileInfo, string photoVirtualPath, Image photoImage)
        {
            WebImage result;

            // TODO: make thumbnailIdentifier configurable
            const string thumbnailIdentifier = "_thumbnail";

            string fileName = Path.GetFileNameWithoutExtension(photoFileInfo.Name);
            string extension = Path.GetExtension(photoFileInfo.Name);
            string thumbnailFileName = string.Format("{0}{1}{2}", fileName, thumbnailIdentifier, extension);
            string thumbnailPhysicalPath = Path.Combine(albumDirectoryInfo.FullName, thumbnailFileName);

            if (File.Exists(thumbnailPhysicalPath))
            {
                using (FileStream thumbnailFileStream = File.OpenRead(thumbnailPhysicalPath))
                {
                    using (Image thumbnailImage = Image.FromStream(thumbnailFileStream))
                    {
                        // TODO: check size and resize and log if necessary

                        result = new WebImage(thumbnailFileName, photoVirtualPath, thumbnailImage.Height, thumbnailImage.Width);
                    }
                }
            }
            else
            {
                // TODO: create thumbnail from first photo

                result = new WebImage(thumbnailFileName, photoVirtualPath, photoImage.Height / 4, photoImage.Width / 4);
            }

            return result;
        }

        private Photo[] GetPhotos(DirectoryInfo albumDirectoryInfo, string albumVirtualPath)
        {
            List<Photo> result = new List<Photo>();

            foreach (FileInfo photoFileInfo in albumDirectoryInfo.GetFiles(_allowablePhotoExtension))
            {
                if (!IsAlbumCover(photoFileInfo.Name))
                {
                    try
                    {
                        using (FileStream photoFileStream = File.OpenRead(photoFileInfo.FullName))
                        {
                            using (Image photoImage = Image.FromStream(photoFileStream))
                            {
                                string photoVirtualPath = VirtualPathUtility.Combine(albumVirtualPath, photoFileInfo.Name);

                                WebImage associatedThumbnail = GetAssociatedThumbnail(albumDirectoryInfo, photoFileInfo, photoVirtualPath, photoImage);

                                result.Add(new Photo(photoFileInfo.Name, photoVirtualPath, photoImage.Height, photoImage.Width, associatedThumbnail));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Trace.TraceWarning(string.Format("Photo {0} is not a valid image file", photoFileInfo.FullName));
                        Trace.TraceError(e.ToString());
                    }
                }
            }

            return result.ToArray();
        }

        private bool IsAlbumCover(string fileName)
        {
            return string.Compare(fileName, _albumCoverFileName, true) == 0;
        }

        private bool IsPortrait(Image image)
        {
            return image.Height > image.Width;
        }

        public SortedList<int, Album[]> Load()
        {
            SortedList<int, Album[]> result = TypedCache.AlbumsGroupedByAlbumYear;

            if (result == null)
            {
                lock (_albumsGroupedByAlbumYearSyncRoot)
                {
                    result = TypedCache.AlbumsGroupedByAlbumYear;

                    if (result == null)
                    {
                        result = GetAlbumsGroupedByAlbumYears();

                        TypedCache.AlbumsGroupedByAlbumYear = result;
                    }
                    else
                    {
                        result = TypedCache.AlbumsGroupedByAlbumYear;
                    }
                }
            }

            return result;
        }

        private void ResizeHeightAndWidth(Image photoImage, int maxDimension, out int height, out int width)
        {
            if (IsPortrait(photoImage))
            {
                float resizeFactor = (float) maxDimension / (float) photoImage.Height;

                height = maxDimension;
                width = Convert.ToInt32(resizeFactor * (float) photoImage.Width);
            }
            else
            {
                float resizeFactor = (float) maxDimension / (float) photoImage.Width;

                height = Convert.ToInt32(resizeFactor * (float) photoImage.Height);
                width = maxDimension;
            }
        }

        #endregion
    }
}