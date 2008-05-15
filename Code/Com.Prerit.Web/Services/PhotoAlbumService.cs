using System;
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

        public SortedList<int, Album[]> GetAlbumsByAlbumYearGroupedByAlbumYear(int albumYear)
        {
            return _photoAlbumLoaderService.Load();
        }

        public SortedList<int, Album[]> GetAlbumsGroupedByAlbumYear()
        {
            return _photoAlbumLoaderService.Load();
        }

        public Photo[] GetPhotosByAlbumYearAndAlbumName(int albumYear, string albumName)
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