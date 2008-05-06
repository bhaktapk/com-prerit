using System;

namespace Com.Prerit.Web
{
    public class Photo
    {
        #region Properties

        public int Height
        {
            get;
            private set;
        }

        public string PhotoName
        {
            get;
            private set;
        }

        public string VirtualPath
        {
            get;
            private set;
        }

        public int Width
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public Photo(string photoName, string virtualPath, int height, int width)
        {
            if (photoName == null)
            {
                throw new ArgumentNullException("photoName");
            }

            if (virtualPath == null)
            {
                throw new ArgumentNullException("virtualPath");
            }

            PhotoName = photoName;
            VirtualPath = virtualPath;
            Height = height;
            Width = width;
        }

        #endregion
    }
}