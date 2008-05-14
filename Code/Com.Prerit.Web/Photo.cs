namespace Com.Prerit.Web
{
    public class Photo : WebImage
    {
        #region Constructors

        public Photo(string caption, string virtualPath, int height, int width)
            : base(caption, virtualPath, height, width)
        {
        }

        #endregion
    }
}