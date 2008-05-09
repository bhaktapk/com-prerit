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

            return new List<Photo>()
            {
                new Photo("example1.jpg", "~/photo_albums/2007/our_first_house/example1.jpg", 112, 150),
                new Photo("example2.jpg", "~/photo_albums/2007/our_first_house/example2.jpg", 150, 112),
                new Photo("example3.jpg", "~/photo_albums/2007/our_first_house/example3.jpg", 112, 150),
                new Photo("example3.jpg", "~/photo_albums/2007/our_first_house/example3.jpg", 112, 150),
                new Photo("example2.jpg", "~/photo_albums/2007/our_first_house/example2.jpg", 150, 112),
                new Photo("example1.jpg", "~/photo_albums/2007/our_first_house/example1.jpg", 112, 150)
            }
            .
            ToArray();
        }

        #endregion
    }
}