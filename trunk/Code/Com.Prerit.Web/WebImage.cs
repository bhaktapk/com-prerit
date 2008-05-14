using System;

namespace Com.Prerit.Web
{
    public class WebImage
    {
        #region Properties

        public string Caption
        {
            get;
            private set;
        }

        public int Height
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

        public WebImage(string caption, string virtualPath, int height, int width)
        {
            if (caption == null)
            {
                throw new ArgumentNullException("caption");
            }

            if (virtualPath == null)
            {
                throw new ArgumentNullException("virtualPath");
            }

            Caption = caption;
            VirtualPath = virtualPath;
            Height = height;
            Width = width;
        }

        #endregion
    }
}