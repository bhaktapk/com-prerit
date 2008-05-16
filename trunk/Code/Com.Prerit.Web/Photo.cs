using System;

namespace Com.Prerit.Web
{
    public class Photo : WebImage
    {
        #region Constructors

        public WebImage Thumbnail
        {
            get;
            private set;
        }

        public Photo(string caption, string virtualPath, int height, int width, WebImage thumbnail)
            : base(caption, virtualPath, height, width)
        {
            if (thumbnail == null)
            {
                throw new ArgumentNullException("thumbnail");
            }

            Thumbnail = thumbnail;
        }

        #endregion
    }
}