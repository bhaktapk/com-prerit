namespace Com.Prerit.Web
{
    public sealed class AspNetSiteMapMarkup : StringEnum<AspNetSiteMapMarkup>
    {
        #region Fields

        public static readonly AspNetSiteMapMarkup ChangeFrequency = new AspNetSiteMapMarkup("changeFrequency");

        public static readonly AspNetSiteMapMarkup Keywords = new AspNetSiteMapMarkup("keywords");

        public static readonly AspNetSiteMapMarkup LastModified = new AspNetSiteMapMarkup("lastModified");

        public static readonly AspNetSiteMapMarkup Priority = new AspNetSiteMapMarkup("priority");

        #endregion

        #region Constructors

        private AspNetSiteMapMarkup(string name)
            : base(name)
        {
        }

        #endregion
    }
}