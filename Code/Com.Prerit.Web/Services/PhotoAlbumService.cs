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

        public AlbumYear FindAlbumYear(int year)
        {
            AlbumYear result;

            Album[] albums = _photoAlbumLoaderService.Load()[year];

            result = new AlbumYear(year, albums);

            return result;
        }

        public AlbumYear[] FindAlbumYears()
        {
            List<AlbumYear> result = new List<AlbumYear>();

            foreach (KeyValuePair<int, Album[]> keyValuePair in _photoAlbumLoaderService.Load())
            {
                result.Add(new AlbumYear(keyValuePair.Key, keyValuePair.Value));
            }

            return result.ToArray();
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