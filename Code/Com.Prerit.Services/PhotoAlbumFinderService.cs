using System;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public class PhotoAlbumFinderService : IPhotoAlbumFinderService
    {
        #region Fields

        private readonly IAlbumYearsCacheItemLoaderService _albumYearsCacheItemLoaderService;

        #endregion

        #region Constructors

        public PhotoAlbumFinderService(IAlbumYearsCacheItemLoaderService albumYearsCacheItemLoaderService)
        {
            _albumYearsCacheItemLoaderService = albumYearsCacheItemLoaderService;
        }

        #endregion

        #region Methods

        public AlbumYear FindAlbumYear(int year)
        {
            AlbumYear result;

            result = Array.Find(_albumYearsCacheItemLoaderService.Load(), albumYear => albumYear.Year == year);

            return result;
        }

        public AlbumYear[] FindAlbumYears()
        {
            AlbumYear[] result;

            result = _albumYearsCacheItemLoaderService.Load();

            return result;
        }

        public Photo[] FindPhotos(int year, string albumName)
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

            AlbumYear[] albumYears = _albumYearsCacheItemLoaderService.Load();

            if (albumYears.Length != 0)
            {
                AlbumYear albumYearFindResult = Array.Find(albumYears, albumYear => albumYear.Year == year);

                if (albumYearFindResult != null)
                {
                    Album albumFindResult = Array.Find(albumYearFindResult.Albums, album => string.Compare(album.AlbumName, albumName, true) == 0);

                    if (albumFindResult != null)
                    {
                        result = albumFindResult.Photos;
                    }
                }
            }

            return result;
        }

        #endregion
    }
}