namespace Com.Prerit.Web
{
    public class CacheKey : StringEnum<CacheKey>
    {
        #region Fields

        private static readonly CacheKey _albumYears = new CacheKey("AlbumYears");

        #endregion

        #region Properties

        public static CacheKey AlbumYears
        {
            get { return _albumYears; }
        }

        #endregion

        #region Constructors

        protected CacheKey(string name)
            : base(name)
        {
        }

        #endregion
    }
}