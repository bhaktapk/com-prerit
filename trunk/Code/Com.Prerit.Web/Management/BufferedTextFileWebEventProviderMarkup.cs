namespace Com.Prerit.Web.Management
{
    public class BufferedTextFileWebEventProviderMarkup : StringEnum<BufferedTextFileWebEventProviderMarkup>
    {
        #region Fields

        private static readonly BufferedTextFileWebEventProviderMarkup _logDirectoryPath = new BufferedTextFileWebEventProviderMarkup("logDirectoryPath");

        private static readonly BufferedTextFileWebEventProviderMarkup _logFileFormat = new BufferedTextFileWebEventProviderMarkup("logFileNameFormat");

        #endregion

        #region Properties

        public static BufferedTextFileWebEventProviderMarkup LogDirectoryPath
        {
            get { return _logDirectoryPath; }
        }

        public static BufferedTextFileWebEventProviderMarkup LogFileFormat
        {
            get { return _logFileFormat; }
        }

        #endregion

        #region Constructors

        protected BufferedTextFileWebEventProviderMarkup(string name)
            : base(name)
        {
        }

        #endregion
    }
}