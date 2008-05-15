using System;

namespace Com.Prerit.Web
{
    public class Photo : WebImage
    {
        #region Constructors

        public WebImage AssociatedThumbnail
        {
            get;
            private set;
        }

        public Photo(string caption, string virtualPath, int height, int width, WebImage associatedThumbnail)
            : base(caption, virtualPath, height, width)
        {
            if (associatedThumbnail == null)
            {
                throw new ArgumentNullException("associatedThumbnail");
            }

            AssociatedThumbnail = associatedThumbnail;
        }

        #endregion
    }
}