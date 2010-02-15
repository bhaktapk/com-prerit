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
        #region Fields

        private readonly ICacheService _cacheService;

        private readonly IDiskInputOutputService _diskInputOutputService;

        private static readonly object AlbumDictionarySyncRoot = new object();

        private static readonly Dictionary<string, object> AlbumSyncRoots = new Dictionary<string, object>();

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

        public IEnumerable<IGrouping<int, Album>> GetAlbumsGroupedByYear()
        {
            IEnumerable<Album> albums = GetAlbums();

            return albums.GroupBy(album => album.Year);
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

        private IEnumerable<string> GetValidAlbumDirPaths(IEnumerable<string> albumDirPaths)
        {
            return (from albumDirPath in albumDirPaths
                    where _diskInputOutputService.FileExists(GetAlbumFilePath(albumDirPath))
                    select albumDirPath).ExecuteQuery();
        }

        private Album LoadAlbum(string dirPath)
        {
            string albumFilePath = GetAlbumFilePath(dirPath);

            var album = _diskInputOutputService.LoadXmlFile<Album>(albumFilePath);

            album.PhotoCount = _diskInputOutputService.GetFiles(dirPath, "*.jpg", SearchOption.TopDirectoryOnly).Count();

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