namespace Com.Prerit.Web
{
    public class GoogleSiteMapMarkup : StringEnum<GoogleSiteMapMarkup>
    {
        #region Fields

        private static readonly GoogleSiteMapMarkup _changefreq = new GoogleSiteMapMarkup("changefreq");

        private static readonly GoogleSiteMapMarkup _lastmod = new GoogleSiteMapMarkup("lastmod");

        private static readonly GoogleSiteMapMarkup _loc = new GoogleSiteMapMarkup("loc");

        private static readonly GoogleSiteMapMarkup _priority = new GoogleSiteMapMarkup("priority");

        private static readonly GoogleSiteMapMarkup _schemaLocation = new GoogleSiteMapMarkup("schemaLocation");

        private static readonly GoogleSiteMapMarkup _schemaLocationValue =
            new GoogleSiteMapMarkup("http://www.google.com/schemas/sitemap/0.84 http://www.google.com/schemas/sitemap/0.84/sitemap.xsd");

        private static readonly GoogleSiteMapMarkup _url = new GoogleSiteMapMarkup("url");

        private static readonly GoogleSiteMapMarkup _urlset = new GoogleSiteMapMarkup("urlset");

        private static readonly GoogleSiteMapMarkup _xmlns = new GoogleSiteMapMarkup("xmlns");

        private static readonly GoogleSiteMapMarkup _xmlnsValue = new GoogleSiteMapMarkup("http://www.google.com/schemas/sitemap/0.84");

        private static readonly GoogleSiteMapMarkup _xsi = new GoogleSiteMapMarkup("xsi");

        private static readonly GoogleSiteMapMarkup _xsiValue = new GoogleSiteMapMarkup("http://www.w3.org/2001/XMLSchema-instance");

        #endregion

        #region Constructors

        protected GoogleSiteMapMarkup(string name)
            : base(name)
        {
        }

        #endregion

        public static GoogleSiteMapMarkup Changefreq
        {
            get { return _changefreq; }
        }

        public static GoogleSiteMapMarkup Lastmod
        {
            get { return _lastmod; }
        }

        public static GoogleSiteMapMarkup Loc
        {
            get { return _loc; }
        }

        public static GoogleSiteMapMarkup Priority
        {
            get { return _priority; }
        }

        public static GoogleSiteMapMarkup SchemaLocation
        {
            get { return _schemaLocation; }
        }

        public static GoogleSiteMapMarkup SchemaLocationValue
        {
            get { return _schemaLocationValue; }
        }

        public static GoogleSiteMapMarkup Url
        {
            get { return _url; }
        }

        public static GoogleSiteMapMarkup Urlset
        {
            get { return _urlset; }
        }

        public static GoogleSiteMapMarkup Xmlns
        {
            get { return _xmlns; }
        }

        public static GoogleSiteMapMarkup XmlnsValue
        {
            get { return _xmlnsValue; }
        }

        public static GoogleSiteMapMarkup Xsi
        {
            get { return _xsi; }
        }

        public static GoogleSiteMapMarkup XsiValue
        {
            get { return _xsiValue; }
        }
    }
}