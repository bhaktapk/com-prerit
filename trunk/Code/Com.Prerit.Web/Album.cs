using System;

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

        public string VirtualPath
        {
            get;
            private set;
        }

        public Photo CoverPhoto
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public Album(string albumName, int albumYear, string virtualPath, Photo coverPhoto)
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

            AlbumName = albumName;
            AlbumYear = albumYear;
            VirtualPath = virtualPath;
            CoverPhoto = coverPhoto;
        }

        #endregion
    }
}