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

        private const string _albumCoverFileName = "album_cover.jpg";

        private const string _allowablePhotoExtension = "*.jpg";

        private const int _maxAlbumCoverDimension = 240;

        private const int _maxThumbnailDimension = 150;

        private const int _minAlbumYear = 1979;

        private const string _thumbnailIdentifier = "_thumbnail";

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

        private WebImage CreateAlbumCover(string albumVirtualPath, string albumCoverPhysicalPath, Photo photo)
        {
            WebImage result;

            using (FileStream photoFileStream = File.OpenRead(HostingEnvironment.MapPath(photo.VirtualPath)))
            {
                using (Image photoImage = Image.FromStream(photoFileStream))
                {
                    int height;
                    int width;

                    DisallowUsageOfEmbeddedThumbnail(photoImage);

                    ResizeHeightAndWidth(photoImage, _maxAlbumCoverDimension, out height, out width);

                    using (Image albumCoverImage = photoImage.GetThumbnailImage(width,
                                                                                height,
                                                                                delegate
                                                                                    {
                                                                                        return false;
                                                                                    },
                                                                                IntPtr.Zero))
                    {
                        string albumCoverVirtualPath = VirtualPathUtility.Combine(albumVirtualPath, _albumCoverFileName);

                        albumCoverImage.Save(albumCoverPhysicalPath, ImageFormat.Jpeg);

                        result = new WebImage(_albumCoverFileName, albumCoverVirtualPath, albumCoverImage.Height, albumCoverImage.Width);
                    }
                }
            }

            return result;
        }

        private WebImage CreateThumbnail(string thumbnailFileName, string photoVirtualPath, string thumbnailPhysicalPath, Image photoImage)
        {
            WebImage result;

            int height;
            int width;

            DisallowUsageOfEmbeddedThumbnail(photoImage);

            ResizeHeightAndWidth(photoImage, _maxThumbnailDimension, out height, out width);

            using (Image thumbnailImage = photoImage.GetThumbnailImage(width,
                                                                       height,
                                                                       delegate
                                                                           {
                                                                               return false;
                                                                           },
                                                                       IntPtr.Zero))
            {
                string thumbnailVirtualPath = VirtualPathUtility.Combine(photoVirtualPath, thumbnailFileName);

                thumbnailImage.Save(thumbnailPhysicalPath, ImageFormat.Jpeg);

                result = new WebImage(thumbnailFileName, thumbnailVirtualPath, thumbnailImage.Height, thumbnailImage.Width);
            }

            return result;
        }

        private void DisallowUsageOfEmbeddedThumbnail(Image image)
        {
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }

        private WebImage GetAlbumCover(string albumVirtualPath, Photo photo)
        {
            WebImage result;

            string albumCoverPhysicalPath = Path.Combine(HostingEnvironment.MapPath(albumVirtualPath), _albumCoverFileName);

            if (!File.Exists(albumCoverPhysicalPath))
            {
                result = CreateAlbumCover(albumVirtualPath, albumCoverPhysicalPath, photo);
            }
            else
            {
                result = UpdateAlbumCover(albumVirtualPath, albumCoverPhysicalPath);
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
                    int maxAlbumYear = DateTime.Today.Year;

                    if (parsedAlbumYear >= _minAlbumYear && parsedAlbumYear <= maxAlbumYear)
                    {
                        string albumYearVirtualPath =
                            VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.Combine(VirtualPath, albumYearDirectoryInfo.Name));

                        result[parsedAlbumYear] = GetAlbums(albumYearDirectoryInfo, parsedAlbumYear, albumYearVirtualPath);
                    }
                    else
                    {
                        Trace.TraceWarning(
                            string.Format("Directory name {0} does not fall between {1} and {2}", albumYearDirectoryInfo.FullName, _minAlbumYear, maxAlbumYear));
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

            string photoFileNameWithoutExtension = Path.GetFileNameWithoutExtension(photoFileInfo.Name);
            string photoExtension = Path.GetExtension(photoFileInfo.Name);
            string thumbnailFileName = photoFileNameWithoutExtension + _thumbnailIdentifier + photoExtension;
            string thumbnailPhysicalPath = Path.Combine(albumDirectoryInfo.FullName, thumbnailFileName);

            if (File.Exists(thumbnailPhysicalPath))
            {
                File.Delete(thumbnailPhysicalPath);
            }

            result = CreateThumbnail(thumbnailFileName, photoVirtualPath, thumbnailPhysicalPath, photoImage);

            return result;
        }

        private Photo[] GetPhotos(DirectoryInfo albumDirectoryInfo, string albumVirtualPath)
        {
            List<Photo> result = new List<Photo>();

            foreach (FileInfo photoFileInfo in albumDirectoryInfo.GetFiles(_allowablePhotoExtension))
            {
                if (!IsAlbumCover(photoFileInfo.Name) && !IsThumbnail(photoFileInfo.Name))
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

        private bool IsThumbnail(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName).EndsWith(_thumbnailIdentifier);
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

        private void OverwriteFile(string albumCoverPhysicalPath, string tempPhysicalPath)
        {
            File.Delete(albumCoverPhysicalPath);
            File.Move(tempPhysicalPath, albumCoverPhysicalPath);
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

        private WebImage UpdateAlbumCover(string albumVirtualPath, string albumCoverPhysicalPath)
        {
            string tempFileName = Path.GetRandomFileName();
            string tempVirtualPath = VirtualPathUtility.Combine(albumVirtualPath, tempFileName);
            string tempPhysicalPath = HostingEnvironment.MapPath(tempVirtualPath);

            WebImage result;

            using (FileStream albumCoverFileStream = File.OpenRead(albumCoverPhysicalPath))
            {
                using (Image albumCoverImage = Image.FromStream(albumCoverFileStream))
                {
                    int height;
                    int width;

                    DisallowUsageOfEmbeddedThumbnail(albumCoverImage);

                    ResizeHeightAndWidth(albumCoverImage, _maxAlbumCoverDimension, out height, out width);

                    using (Image newAlbumCoverImage = albumCoverImage.GetThumbnailImage(width,
                                                                                        height,
                                                                                        delegate
                                                                                            {
                                                                                                return false;
                                                                                            },
                                                                                        IntPtr.Zero))
                    {
                        string albumCoverVirtualPath = VirtualPathUtility.Combine(albumVirtualPath, _albumCoverFileName);

                        newAlbumCoverImage.Save(tempPhysicalPath, ImageFormat.Jpeg);

                        result = new WebImage(_albumCoverFileName, albumCoverVirtualPath, newAlbumCoverImage.Height, newAlbumCoverImage.Width);
                    }
                }
            }

            OverwriteFile(albumCoverPhysicalPath, tempPhysicalPath);

            return result;
        }

        #endregion
    }
}