using System;

namespace Com.Prerit.Domain
{
    public class Photo : WebImage
    {
        #region Constructors

        public WebImage Thumbnail
        {
            get;
            private set;
        }


        public WebImage ResizedImage
        {
            get;
            private set;
        }

        public Photo(string caption, string virtualPath, int height, int width, WebImage thumbnail, WebImage resizedImage)
            : base(caption, virtualPath, height, width)
        {
            if (thumbnail == null)
            {
                throw new ArgumentNullException("thumbnail");
            }

            if (resizedImage == null)
            {
                throw new ArgumentNullException("resizedImage");
            }

            Thumbnail = thumbnail;
            ResizedImage = resizedImage;
        }

        #endregion
    }
}