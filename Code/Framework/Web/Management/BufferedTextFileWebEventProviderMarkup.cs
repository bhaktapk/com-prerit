namespace Framework.Web
{
    public sealed class BufferedTextFileWebEventProviderMarkup : StringEnum<BufferedTextFileWebEventProviderMarkup>
    {
        #region Fields

        public static readonly BufferedTextFileWebEventProviderMarkup LogDirectoryPath = new BufferedTextFileWebEventProviderMarkup("logDirectoryPath");

        public static readonly BufferedTextFileWebEventProviderMarkup LogFileFormat = new BufferedTextFileWebEventProviderMarkup("logFileNameFormat");

        #endregion

        #region Constructors

        private BufferedTextFileWebEventProviderMarkup(string name)
            : base(name)
        {
        }

        #endregion
    }
}