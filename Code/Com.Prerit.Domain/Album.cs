using System;
using System.Web;

namespace Com.Prerit.Domain
{
    public class Album
    {
        #region Properties

        public WebImage AlbumCover
        {
            get;
            private set;
        }

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

        public Album(string albumName, int albumYear, string virtualPath, WebImage albumCover, Photo[] photos)
        {
            if (albumName == null)
            {
                throw new ArgumentNullException("albumName");
            }

            if (virtualPath == null)
            {
                throw new ArgumentNullException("virtualPath");
            }

            if (!VirtualPathUtility.IsAbsolute(virtualPath) && !VirtualPathUtility.IsAppRelative(virtualPath))
            {
                throw new ArgumentException("Path must either be an absolute virtual path or app relative virtual path", "virtualPath");
            }

            if (albumCover == null)
            {
                throw new ArgumentNullException("albumCover");
            }

            if (photos == null)
            {
                throw new ArgumentNullException("photos");
            }

            AlbumName = albumName;
            AlbumYear = albumYear;
            VirtualPath = virtualPath;
            AlbumCover = albumCover;
            Photos = photos;
        }

        #endregion
    }
}