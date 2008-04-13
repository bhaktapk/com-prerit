namespace Framework.Web
{
	public sealed class AspNetSiteMapMarkup : StringEnum<AspNetSiteMapMarkup>
	{
		public static readonly AspNetSiteMapMarkup ChangeFrequency = new AspNetSiteMapMarkup("changeFrequency");

		public static readonly AspNetSiteMapMarkup Keywords = new AspNetSiteMapMarkup("keywords");

		public static readonly AspNetSiteMapMarkup LastModified = new AspNetSiteMapMarkup("lastModified");

		public static readonly AspNetSiteMapMarkup Priority = new AspNetSiteMapMarkup("priority");

		private AspNetSiteMapMarkup(string name)
			: base(name)
		{
		}
	}
}
