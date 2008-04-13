namespace Framework.Web
{
	public sealed class BufferedTextFileWebEventProviderMarkup : StringEnum<BufferedTextFileWebEventProviderMarkup>
	{
		public static readonly BufferedTextFileWebEventProviderMarkup LogDirectoryPath = new BufferedTextFileWebEventProviderMarkup("logDirectoryPath");

		public static readonly BufferedTextFileWebEventProviderMarkup LogFileFormat = new BufferedTextFileWebEventProviderMarkup("logFileNameFormat");

		private BufferedTextFileWebEventProviderMarkup(string name)
			: base(name)
		{
		}
	}
}
