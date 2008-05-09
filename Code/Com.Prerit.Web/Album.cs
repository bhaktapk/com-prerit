using System;
using System.Diagnostics;

namespace Com.Prerit.Web
{
    public class Album
    {
        #region Properties

        public string AlbumName
        {
            get;
            private set;
        }

        public int AlbumYear
        {
            get;
            private set;
        }

        public Photo CoverPhoto
        {
            get;
            private set;
        }

        public Photo[] Photos
        {
            get;
            private set;
        }

        public string VirtualPath
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public Album(string albumName, int albumYear, string virtualPath, Photo coverPhoto, Photo[] photos)
        {
            if (albumName == null)
            {
                throw new ArgumentNullException("albumName");
            }

            if (virtualPath == null)
            {
                throw new ArgumentNullException("virtualPath");
            }

            if (coverPhoto == null)
            {
                throw new ArgumentNullException("coverPhoto");
            }

            if (photos == null)
            {
                throw new ArgumentNullException("photos");
            }

            if (photos.Length == 0)
            {
                throw new ArgumentException("Parameter cannot be empty", "photos");
            }

            if (!IsCoverPhotoInAlbumPhotos(coverPhoto, photos))
            {
                throw new ArgumentException("The cover photo must belong to the album's photos", "coverPhoto");
            }

            AlbumName = albumName;
            AlbumYear = albumYear;
            VirtualPath = virtualPath;
            CoverPhoto = coverPhoto;
            Photos = photos;
        }

        #endregion

        #region Methods

        private bool IsCoverPhotoInAlbumPhotos(Photo photo, Photo[] albumPhotos)
        {
            Debug.Assert(photo != null);
            Debug.Assert(albumPhotos != null);
            Debug.Assert(albumPhotos.Length != 0);

            // TODO: use lamba expression once Resharper recognizes C# 3.0 syntax
            Photo photoFindResult = Array.Find(albumPhotos,
                                               delegate(Photo obj)
                                                   {
                                                       return obj == photo;
                                                   });

            return photoFindResult != null;
        }

        #endregion
    }
}