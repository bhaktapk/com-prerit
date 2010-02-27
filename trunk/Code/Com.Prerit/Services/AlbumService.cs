using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Com.Prerit.Core;
using Com.Prerit.Domain;

using Links;

namespace Com.Prerit.Services
{
    public class AlbumService : IAlbumService
    {
        #region Constants

        private const string AlbumPortraitFileName = "AlbumPortrait.jpg";

        private const int MaxAlbumPortraitDimension = 240;

        private const int MaxThumbnailDimension = 150;

        private const int MaxWebOptimizedDimension = 640;

        private const string ThumbnailDirectoryName = "Thumbnails";

        private const string WebOptimizedDirectoryName = "WebOptimized";

        #endregion

        #region Fields

        private readonly ICacheService _cacheService;

        private readonly IDiskInputOutputService _diskInputOutputService;

        private static readonly object AlbumDictionarySyncRoot = new object();

        private static readonly Dictionary<string, object> AlbumSyncRoots = new Dictionary<string, object>();

        private static readonly object ImageDictionarySyncRoot = new object();

        private static readonly Dictionary<string, object> ImageSyncRoots = new Dictionary<string, object>();

        #endregion

        #region Constructors

        public AlbumService(ICacheService cacheService, IDiskInputOutputService diskInputOutputService)
        {
            if (cacheService == null)
            {
                throw new ArgumentNullException("cacheService");
            }

            if (diskInputOutputService == null)
            {
                throw new ArgumentNullException("diskInputOutputService");
            }

            _cacheService = cacheService;
            _diskInputOutputService = diskInputOutputService;
        }

        #endregion

        #region Methods

        private object AddAlbumSyncRoot(string dirPath)
        {
            var albumSyncRoot = new object();

            AlbumSyncRoots.Add(dirPath, albumSyncRoot);

            return albumSyncRoot;
        }

        private object AddImageSyncRoot(string filePath)
        {
            var imageSyncRoot = new object();

            ImageSyncRoots.Add(filePath, imageSyncRoot);

            return imageSyncRoot;
        }

        private void EnsureImageExists(int maxDimension, string sourceFilePath, string destinationFilePath)
        {
            if (_diskInputOutputService.FileExists(destinationFilePath))
            {
                return;
            }

            lock (GetImageSyncRoot(destinationFilePath))
            {
                if (_diskInputOutputService.FileExists(destinationFilePath))
                {
                    return;
                }

                _diskInputOutputService.ResizeImage(maxDimension, sourceFilePath, destinationFilePath);
            }
        }

        public Album GetAlbum(int year, string slug)
        {
            IEnumerable<Album> albums = GetAlbums();

            return albums.Where(album => album.Year == year && string.Compare(album.Slug, slug, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
        }

        private Album GetAlbum(string dirPath)
        {
            Album album = _cacheService.GetAlbum(dirPath);

            if (album != null)
            {
                return album;
            }

            lock (GetAlbumSyncRoot(dirPath))
            {
                return _cacheService.GetAlbum(dirPath) ?? LoadAlbum(dirPath);
            }
        }

        private string GetAlbumFilePath(string dirPath)
        {
            return Path.Combine(dirPath, "Album.xml");
        }

        private IEnumerable<Album> GetAlbumObjects(IEnumerable<string> validAlbumDirPaths)
        {
            return (from albumDirPath in validAlbumDirPaths
                    select GetAlbum(albumDirPath)).ExecuteQuery();
        }

        public WebImage GetAlbumPhoto(int year, string slug, int photoIndex, AlbumPhotoType albumPhotoType)
        {
            Album album = GetAlbum(year, slug);

            if (album == null)
            {
                return null;
            }

            int maxDimension;

            string resizedFilePath;

            string photoFilePath = GetValidPhotoFilePaths(album.DirectoryPath).ElementAt(photoIndex);

            switch (albumPhotoType)
            {
                case AlbumPhotoType.Thumbnail:
                    maxDimension = MaxThumbnailDimension;
                    resizedFilePath = GetAlbumPhotoThumbnailFilePath(album.DirectoryPath, photoFilePath);
                    break;
                case AlbumPhotoType.WebOptimized:
                    maxDimension = MaxWebOptimizedDimension;
                    resizedFilePath = GetAlbumPhotoWebOptimizedFilePath(album.DirectoryPath, photoFilePath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("albumPhotoType");
            }

            EnsureImageExists(maxDimension, photoFilePath, resizedFilePath);

            return new WebImage
                       {
                           FilePath = resizedFilePath
                       };
        }

        private string GetAlbumPhotoThumbnailFilePath(string albumDirectoryPath, string photoFilePath)
        {
            return Path.Combine(Path.Combine(albumDirectoryPath, ThumbnailDirectoryName), Path.GetFileName(photoFilePath));
        }

        private string GetAlbumPhotoWebOptimizedFilePath(string albumDirectoryPath, string photoFilePath)
        {
            return Path.Combine(Path.Combine(albumDirectoryPath, WebOptimizedDirectoryName), Path.GetFileName(photoFilePath));
        }

        public WebImage GetAlbumPortrait(int year, string slug)
        {
            Album album = GetAlbum(year, slug);

            if (album == null)
            {
                return null;
            }

            string portraitFilePath = GetAlbumPortraitFilePath(album.DirectoryPath);

            string thumbnailFilePath = GetAlbumPortraitThumbnailFilePath(album.DirectoryPath);

            EnsureImageExists(MaxAlbumPortraitDimension, portraitFilePath, thumbnailFilePath);

            return new WebImage
                       {
                           FilePath = thumbnailFilePath
                       };
        }

        private string GetAlbumPortraitFilePath(string dirPath)
        {
            return Path.Combine(dirPath, AlbumPortraitFileName);
        }

        private string GetAlbumPortraitThumbnailFilePath(string albumDirectoryPath)
        {
            return Path.Combine(Path.Combine(albumDirectoryPath, ThumbnailDirectoryName), AlbumPortraitFileName);
        }

        public IEnumerable<Album> GetAlbums()
        {
            IEnumerable<Album> albums = _cacheService.GetAlbums();

            if (albums != null)
            {
                return albums;
            }

            lock (AlbumDictionarySyncRoot)
            {
                return _cacheService.GetAlbums() ?? LoadAlbums();
            }
        }

        public IEnumerable<Album> GetAlbums(int year)
        {
            IEnumerable<Album> albums = GetAlbums();

            return albums.Where(album => album.Year == year);
        }

        public IEnumerable<string> GetAlbumSlugs(int year)
        {
            IEnumerable<Album> albums = GetAlbums(year);

            return albums.Select(album => album.Slug);
        }

        private object GetAlbumSyncRoot(string dirPath)
        {
            object albumSyncRoot;

            if (AlbumSyncRoots.TryGetValue(dirPath, out albumSyncRoot))
            {
                return albumSyncRoot;
            }

            lock (AlbumDictionarySyncRoot)
            {
                if (AlbumSyncRoots.TryGetValue(dirPath, out albumSyncRoot))
                {
                    return albumSyncRoot;
                }

                return AddAlbumSyncRoot(dirPath);
            }
        }

        public IEnumerable<int> GetAlbumYears()
        {
            IEnumerable<Album> albums = GetAlbums();

            return albums.Select(album => album.Year).Distinct();
        }

        private object GetImageSyncRoot(string filePath)
        {
            object imageSyncRoot;

            if (ImageSyncRoots.TryGetValue(filePath, out imageSyncRoot))
            {
                return imageSyncRoot;
            }

            lock (ImageDictionarySyncRoot)
            {
                if (ImageSyncRoots.TryGetValue(filePath, out imageSyncRoot))
                {
                    return imageSyncRoot;
                }

                return AddImageSyncRoot(filePath);
            }
        }

        private IEnumerable<string> GetValidAlbumDirPaths(IEnumerable<string> albumDirPaths)
        {
            return (from albumDirPath in albumDirPaths
                    where
                        _diskInputOutputService.FileExists(GetAlbumFilePath(albumDirPath)) &&
                        _diskInputOutputService.FileExists(GetAlbumPortraitFilePath(albumDirPath)) && GetValidPhotoFilePaths(albumDirPath).Count() > 0
                    select albumDirPath).ExecuteQuery();
        }

        private IEnumerable<string> GetValidPhotoFilePaths(string dirPath)
        {
            return from filePath in _diskInputOutputService.GetFiles(dirPath, "*.jpg", SearchOption.TopDirectoryOnly)
                   where string.Compare(Path.GetFileName(filePath), AlbumPortraitFileName, StringComparison.OrdinalIgnoreCase) != 0
                   select filePath;
        }

        private Album LoadAlbum(string dirPath)
        {
            string albumFilePath = GetAlbumFilePath(dirPath);

            var album = _diskInputOutputService.LoadXmlFile<Album>(albumFilePath);

            album.DirectoryPath = dirPath;
            album.PhotoCount = GetValidPhotoFilePaths(dirPath).Count();

            _cacheService.SetAlbum(album, dirPath);

            return album;
        }

        private IEnumerable<Album> LoadAlbums()
        {
            string albumRootDirPath = _diskInputOutputService.MapPath(App_Data.Albums.Url());

            IEnumerable<string> albumDirPaths = _diskInputOutputService.GetDirectories(albumRootDirPath);

            IEnumerable<string> validAlbumDirPaths = GetValidAlbumDirPaths(albumDirPaths);

            IEnumerable<Album> albums = GetAlbumObjects(validAlbumDirPaths);

            _cacheService.SetAlbums(albums, albumRootDirPath, albumDirPaths, validAlbumDirPaths);

            return albums;
        }

        #endregion
    }
}