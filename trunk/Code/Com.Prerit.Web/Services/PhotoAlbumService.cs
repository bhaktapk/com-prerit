﻿using System;
using System.Collections.Generic;

namespace Com.Prerit.Web.Services
{
    public class PhotoAlbumService : IPhotoAlbumService
    {
        #region Fields

        private readonly IPhotoAlbumLoaderService _photoAlbumLoaderService;

        #endregion

        #region Constructors

        public PhotoAlbumService(IPhotoAlbumLoaderService photoAlbumLoaderService)
        {
            _photoAlbumLoaderService = photoAlbumLoaderService;
        }

        #endregion

        #region Methods

        public SortedList<int, Album[]> FindAlbums(int albumYear)
        {
            SortedList<int, Album[]> result = new SortedList<int, Album[]>();

            Album[] albums = _photoAlbumLoaderService.Load()[albumYear];

            result.Add(albumYear, albums);

            return result;
        }

        public SortedList<int, Album[]> FindAlbums()
        {
            return _photoAlbumLoaderService.Load();
        }

        public Photo[] FindPhotos(int albumYear, string albumName)
        {
            if (albumName == null)
            {
                throw new ArgumentNullException("albumName");
            }

            if (albumName == string.Empty)
            {
                throw new ArgumentException("String cannot be empty", "albumName");
            }

            Photo[] result = new Photo[0];

            Album[] albums = _photoAlbumLoaderService.Load()[albumYear];

            if (albums != null && albums.Length != 0)
            {
                Album albumFindResult = Array.Find(albums,
                                                   delegate(Album album)
                                                       {
                                                           return string.Compare(album.AlbumName, albumName, true) == 0;
                                                       });

                if (albumFindResult != null)
                {
                    result = albumFindResult.Photos;
                }
            }

            return result;
        }

        #endregion
    }
}