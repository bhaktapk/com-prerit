namespace Com.Prerit.Web
{
    public class AspNetSiteMapMarkup : StringEnum<AspNetSiteMapMarkup>
    {
        #region Fields

        private static readonly AspNetSiteMapMarkup _changeFrequency = new AspNetSiteMapMarkup("changeFrequency");

        private static readonly AspNetSiteMapMarkup _keywords = new AspNetSiteMapMarkup("keywords");

        private static readonly AspNetSiteMapMarkup _lastModified = new AspNetSiteMapMarkup("lastModified");

        private static readonly AspNetSiteMapMarkup _priority = new AspNetSiteMapMarkup("priority");

        #endregion

        #region Constructors

        protected AspNetSiteMapMarkup(string name)
            : base(name)
        {
        }

        #endregion

        public static AspNetSiteMapMarkup ChangeFrequency
        {
            get { return _changeFrequency; }
        }

        public static AspNetSiteMapMarkup Keywords
        {
            get { return _keywords; }
        }

        public static AspNetSiteMapMarkup LastModified
        {
            get { return _lastModified; }
        }

        public static AspNetSiteMapMarkup Priority
        {
            get { return _priority; }
        }
    }
}