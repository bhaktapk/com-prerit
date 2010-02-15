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

        private Album GetAlbum(string dirPath)
        {
            if (_cacheService.GetAlbum(dirPath) == null)
            {
                lock (GetAlbumSyncRoot(dirPath))
                {
                    if (_cacheService.GetAlbum(dirPath) == null)
                    {
                        string albumFilePath = GetAlbumFilePath(dirPath);

                        var album = _diskInputOutputService.LoadXmlFile<Album>(albumFilePath);

                        album.PhotoCount = _diskInputOutputService.GetFiles(dirPath, "*.jpg", SearchOption.TopDirectoryOnly).Count();

                        _cacheService.SetAlbum(album, dirPath);
                    }
                }
            }

            return _cacheService.GetAlbum(dirPath);
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
            if (_cacheService.GetAlbums() == null)
            {
                lock (AlbumDictionarySyncRoot)
                {
                    if (_cacheService.GetAlbums() == null)
                    {
                        string albumRootDirPath = _diskInputOutputService.MapPath(App_Data.Albums.Url());

                        IEnumerable<string> albumDirPaths = _diskInputOutputService.GetDirectories(albumRootDirPath);

                        IEnumerable<string> validAlbumDirPaths = GetValidAlbumDirPaths(albumDirPaths);

                        IEnumerable<Album> albums = GetAlbumObjects(validAlbumDirPaths);

                        _cacheService.SetAlbums(albums, albumRootDirPath, albumDirPaths, validAlbumDirPaths);
                    }
                }
            }

            return _cacheService.GetAlbums();
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
            if (!AlbumSyncRoots.ContainsKey(dirPath))
            {
                lock (AlbumDictionarySyncRoot)
                {
                    if (!AlbumSyncRoots.ContainsKey(dirPath))
                    {
                        AlbumSyncRoots.Add(dirPath, new object());
                    }
                }
            }

            return AlbumSyncRoots[dirPath];
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

        #endregion
    }
}