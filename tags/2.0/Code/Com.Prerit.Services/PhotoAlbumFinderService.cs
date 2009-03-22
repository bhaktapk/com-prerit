using System;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public class PhotoAlbumFinderService : IPhotoAlbumFinderService
    {
        private readonly AlbumYear[] _albumYears;

        #region Constructors

        public PhotoAlbumFinderService(AlbumYear[] albumYears)
        {
            if (albumYears == null)
            {
                throw new ArgumentNullException("albumYears");
            }

            _albumYears = albumYears;
        }

        #endregion

        #region Methods

        public AlbumYear FindAlbumYear(int year)
        {
            AlbumYear result;

            result = Array.Find(_albumYears, albumYear => albumYear.Year == year);

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

            AlbumYear albumYearFindResult = Array.Find(_albumYears, albumYear => albumYear.Year == year);

            if (albumYearFindResult != null)
            {
                Album albumFindResult = Array.Find(albumYearFindResult.Albums, album => string.Compare(album.AlbumName, albumName, true) == 0);

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