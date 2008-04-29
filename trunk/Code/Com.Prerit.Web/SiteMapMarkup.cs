namespace Com.Prerit.Web
{
    public class SiteMapMarkup : StringEnum<SiteMapMarkup>
    {
        #region Fields

        private static readonly SiteMapMarkup _accessKey = new SiteMapMarkup("accessKey");

        private static readonly SiteMapMarkup _changeFrequency = new SiteMapMarkup("changeFrequency");

        private static readonly SiteMapMarkup _keywords = new SiteMapMarkup("keywords");

        private static readonly SiteMapMarkup _lastModified = new SiteMapMarkup("lastModified");

        private static readonly SiteMapMarkup _priority = new SiteMapMarkup("priority");

        #endregion

        #region Properties

        public static SiteMapMarkup AccessKey
        {
            get { return _accessKey; }
        }

        public static SiteMapMarkup ChangeFrequency
        {
            get { return _changeFrequency; }
        }

        public static SiteMapMarkup Keywords
        {
            get { return _keywords; }
        }

        public static SiteMapMarkup LastModified
        {
            get { return _lastModified; }
        }

        public static SiteMapMarkup Priority
        {
            get { return _priority; }
        }

        #endregion

        #region Constructors

        protected SiteMapMarkup(string name)
            : base(name)
        {
        }

        #endregion
    }
}