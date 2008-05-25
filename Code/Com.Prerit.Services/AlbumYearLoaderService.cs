using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Hosting;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public class AlbumYearLoaderService : IAlbumYearLoaderService
    {
        #region Constants

        private const string _albumCoverFileName = "album_cover.jpg";

        private const string _allowablePhotoExtension = "*.jpg";

        private const int _maxAlbumCoverDimension = 240;

        private const int _maxResizedImageDimension = 480;

        private const int _maxThumbnailDimension = 150;

        private const int _minAlbumYear = 1979;

        private const string _resizedImageIdentifier = "_resized";

        private const string _thumbnailIdentifier = "_thumbnail";

        #endregion

        #region Fields

        private readonly IImageEditorService _imageEditorService;

        #endregion

        #region Properties

        public string VirtualPath
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public AlbumYearLoaderService(string virtualPath, IImageEditorService imageEditorService)
        {
            if (virtualPath == null)
            {
                throw new ArgumentNullException("virtualPath");
            }

            if (!VirtualPathUtility.IsAbsolute(virtualPath) && !VirtualPathUtility.IsAppRelative(virtualPath))
            {
                throw new ArgumentException("Path must either be an absolute virtual path or app relative virtual path", "virtualPath");
            }

            if (imageEditorService == null)
            {
                throw new ArgumentNullException("imageEditorService");
            }

            VirtualPath = virtualPath;
            _imageEditorService = imageEditorService;
        }

        #endregion

        #region Methods

        private WebImage CreateAlbumCover(int maxDimension, string fileName, string virtualPath, string physicalPath, Photo photo)
        {
            WebImage result;

            string photoPhysicalPath = HostingEnvironment.MapPath(photo.VirtualPath);

            result = CreateScaledImage(maxDimension, fileName, virtualPath, physicalPath, photoPhysicalPath);

            return result;
        }

        private WebImage CreateScaledImage(int maxDimension, string fileName, string virtualPath, string physicalPath, string originalImagePhysicalPath)
        {
            WebImage result;

            using (Image scaledImage = _imageEditorService.ScaleAndSaveImage(maxDimension, physicalPath, originalImagePhysicalPath))
            {
                result = new WebImage(fileName, virtualPath, scaledImage.Height, scaledImage.Width);
            }

            return result;
        }

        private WebImage CreateScaledImage(int maxDimension, string fileName, string virtualPath, string physicalPath, Image originalImage)
        {
            WebImage result;

            using (Image scaledImage = _imageEditorService.ScaleAndSaveImage(maxDimension, physicalPath, originalImage))
            {
                result = new WebImage(fileName, virtualPath, scaledImage.Height, scaledImage.Width);
            }

            return result;
        }

        private WebImage GetAlbumCover(string albumVirtualPath, Photo[] photos)
        {
            WebImage result;

            if (photos.Length != 0)
            {
                string albumCoverVirtualPath = VirtualPathUtility.Combine(albumVirtualPath, _albumCoverFileName);
                string albumCoverPhysicalPath = HostingEnvironment.MapPath(albumCoverVirtualPath);

                if (File.Exists(albumCoverPhysicalPath))
                {
                    result = UpdateAlbumCover(albumVirtualPath, albumCoverVirtualPath, albumCoverPhysicalPath);
                }
                else
                {
                    result = CreateAlbumCover(_maxAlbumCoverDimension, _albumCoverFileName, albumCoverVirtualPath, albumCoverPhysicalPath, photos[0]);
                }
            }
            else
            {
                result = GetDefaultAlbumCover();

                Trace.TraceWarning(string.Format("Directory {0} contained no useable photos", albumVirtualPath));
            }

            return result;
        }

        private Album[] GetAlbums(int albumYear, string albumYearVirtualPath, DirectoryInfo albumYearDirectoryInfo)
        {
            List<Album> result = new List<Album>();

            foreach (DirectoryInfo albumDirectoryInfo in GetNonHiddenSubDirectories(albumYearDirectoryInfo))
            {
                string albumName = albumDirectoryInfo.Name;
                string albumVirtualPath = VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.Combine(albumYearVirtualPath, albumName));

                Photo[] photos = GetPhotos(albumVirtualPath, albumDirectoryInfo);

                WebImage albumCover = GetAlbumCover(albumVirtualPath, photos);

                result.Add(new Album(albumName, albumYear, albumVirtualPath, albumCover, photos));
            }

            return result.ToArray();
        }

        private WebImage GetDefaultAlbumCover()
        {
            WebImage result;

            const string fileName = "default_album_cover.jpg";
            const string virtualPath = "~/images/" + fileName;

            string physicalPath = HostingEnvironment.MapPath(virtualPath);

            result = GetExistingScaledImage(fileName, virtualPath, physicalPath);

            return result;
        }

        private WebImage GetExistingScaledImage(string imageFileName, string imageVirtualPath, string imagePhysicalPath)
        {
            WebImage result;

            using (Image image = Image.FromFile(imagePhysicalPath))
            {
                result = new WebImage(imageFileName, imageVirtualPath, image.Height, image.Width);
            }

            return result;
        }

        private DirectoryInfo[] GetNonHiddenSubDirectories(DirectoryInfo directoryInfo)
        {
            List<DirectoryInfo> result = new List<DirectoryInfo>();

            foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
            {
                if ((subDirectoryInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    result.Add(subDirectoryInfo);
                }
            }

            return result.ToArray();
        }

        private Photo[] GetPhotos(string albumVirtualPath, DirectoryInfo albumDirectoryInfo)
        {
            List<Photo> result = new List<Photo>();

            foreach (FileInfo photoFileInfo in albumDirectoryInfo.GetFiles(_allowablePhotoExtension))
            {
                if (!IsAlbumCover(photoFileInfo.Name) && !IsThumbnail(photoFileInfo.Name) && !IsResizedImage(photoFileInfo.Name))
                {
                    using (Image photoImage = Image.FromFile(photoFileInfo.FullName))
                    {
                        string photoVirtualPath = VirtualPathUtility.Combine(albumVirtualPath, photoFileInfo.Name);

                        WebImage thumbnail = GetScaledImage(_maxThumbnailDimension, _thumbnailIdentifier, photoVirtualPath, photoFileInfo, photoImage);
                        WebImage resizedImage = GetScaledImage(_maxResizedImageDimension, _resizedImageIdentifier, photoVirtualPath, photoFileInfo, photoImage);

                        result.Add(new Photo(photoFileInfo.Name, photoVirtualPath, photoImage.Height, photoImage.Width, thumbnail, resizedImage));
                    }
                }
            }

            return result.ToArray();
        }

        private WebImage GetScaledImage(int maxDimension, string scaledImageIdentifier, string photoVirtualPath, FileInfo photoFileInfo, Image photoImage)
        {
            WebImage result;

            string photoFileNameWithoutExtension = Path.GetFileNameWithoutExtension(photoFileInfo.Name);
            string photoExtension = Path.GetExtension(photoFileInfo.Name);
            string fileName = photoFileNameWithoutExtension + scaledImageIdentifier + photoExtension;
            string virtualPath = VirtualPathUtility.Combine(photoVirtualPath, fileName);
            string physicalPath = HostingEnvironment.MapPath(virtualPath);

            if (File.Exists(physicalPath))
            {
                result = GetExistingScaledImage(fileName, virtualPath, physicalPath);
            }
            else
            {
                result = CreateScaledImage(maxDimension, fileName, virtualPath, physicalPath, photoImage);
            }

            return result;
        }

        private bool IsAlbumCover(string fileName)
        {
            return string.Compare(fileName, _albumCoverFileName, true) == 0;
        }

        private bool IsResizedImage(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName).EndsWith(_resizedImageIdentifier);
        }

        private bool IsThumbnail(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName).EndsWith(_thumbnailIdentifier);
        }

        public AlbumYear[] Load()
        {
            List<AlbumYear> result = new List<AlbumYear>();

            DirectoryInfo photoAlbumsDirectoryInfo = new DirectoryInfo(HostingEnvironment.MapPath(VirtualPath));

            foreach (DirectoryInfo albumYearDirectoryInfo in GetNonHiddenSubDirectories(photoAlbumsDirectoryInfo))
            {
                int parsedAlbumYear;

                if (int.TryParse(albumYearDirectoryInfo.Name, out parsedAlbumYear))
                {
                    int maxAlbumYear = DateTime.Today.Year;

                    if (parsedAlbumYear >= _minAlbumYear && parsedAlbumYear <= maxAlbumYear)
                    {
                        string albumYearVirtualPath =
                            VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.Combine(VirtualPath, albumYearDirectoryInfo.Name));

                        Album[] albums = GetAlbums(parsedAlbumYear, albumYearVirtualPath, albumYearDirectoryInfo);

                        result.Add(new AlbumYear(parsedAlbumYear, albumYearVirtualPath, albums));
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

            return result.ToArray();
        }

        private void OverwriteFile(string oldFilePhysicalPath, string newFilePhysicalPath)
        {
            File.Delete(oldFilePhysicalPath);
            File.Move(newFilePhysicalPath, oldFilePhysicalPath);
        }

        private WebImage UpdateAlbumCover(string albumVirtualPath, string albumCoverVirtualPath, string albumCoverPhysicalPath)
        {
            WebImage result;

            string tempFileName = Path.GetRandomFileName();
            string tempVirtualPath = VirtualPathUtility.Combine(albumVirtualPath, tempFileName);
            string tempPhysicalPath = HostingEnvironment.MapPath(tempVirtualPath);

            result = CreateScaledImage(_maxAlbumCoverDimension, _albumCoverFileName, albumCoverVirtualPath, tempPhysicalPath, albumCoverPhysicalPath);

            OverwriteFile(albumCoverPhysicalPath, tempPhysicalPath);

            return result;
        }

        #endregion
    }
}