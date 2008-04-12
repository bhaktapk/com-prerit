using Framework;

namespace Framework.Web
{
	public sealed class GoogleSiteMapMarkup : StringEnum<GoogleSiteMapMarkup>
	{
		public static readonly GoogleSiteMapMarkup Changefreq = new GoogleSiteMapMarkup("changefreq");

		public static readonly GoogleSiteMapMarkup Lastmod = new GoogleSiteMapMarkup("lastmod");

		public static readonly GoogleSiteMapMarkup Loc = new GoogleSiteMapMarkup("loc");

		public static readonly GoogleSiteMapMarkup Priority = new GoogleSiteMapMarkup("priority");

		public static readonly GoogleSiteMapMarkup SchemaLocation = new GoogleSiteMapMarkup("schemaLocation");

		public static readonly GoogleSiteMapMarkup SchemaLocationValue = new GoogleSiteMapMarkup("http://www.google.com/schemas/sitemap/0.84 http://www.google.com/schemas/sitemap/0.84/sitemap.xsd");

		public static readonly GoogleSiteMapMarkup Url = new GoogleSiteMapMarkup("url");

		public static readonly GoogleSiteMapMarkup Urlset = new GoogleSiteMapMarkup("urlset");

		public static readonly GoogleSiteMapMarkup Xmlns = new GoogleSiteMapMarkup("xmlns");

		public static readonly GoogleSiteMapMarkup XmlnsValue = new GoogleSiteMapMarkup("http://www.google.com/schemas/sitemap/0.84");

		public static readonly GoogleSiteMapMarkup Xsi = new GoogleSiteMapMarkup("xsi");

		public static readonly GoogleSiteMapMarkup XsiValue = new GoogleSiteMapMarkup("http://www.w3.org/2001/XMLSchema-instance");

		private GoogleSiteMapMarkup(string name)
			: base(name)
		{
		}
	}
}
