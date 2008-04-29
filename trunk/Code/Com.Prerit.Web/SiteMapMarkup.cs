namespace Com.Prerit.Web
{
    public sealed class SiteMapMarkup : StringEnum<SiteMapMarkup>
    {
        #region Fields

        public static readonly SiteMapMarkup AccessKey = new SiteMapMarkup("accessKey");

        public static readonly SiteMapMarkup ChangeFrequency = new SiteMapMarkup("changeFrequency");

        public static readonly SiteMapMarkup Keywords = new SiteMapMarkup("keywords");

        public static readonly SiteMapMarkup LastModified = new SiteMapMarkup("lastModified");

        public static readonly SiteMapMarkup Priority = new SiteMapMarkup("priority");

        #endregion

        #region Constructors

        private SiteMapMarkup(string name)
            : base(name)
        {
        }

        #endregion
    }
}