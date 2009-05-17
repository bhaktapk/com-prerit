using Com.Prerit.Core;

namespace Com.Prerit.Services.Caching
{
    public class CacheKey : StringEnum<CacheKey>
    {
        #region Properties

        public static CacheKey AlbumYears { get; private set; }

        #endregion

        #region Constructors

        static CacheKey()
        {
            AlbumYears = new CacheKey("AlbumYears");
        }

        protected CacheKey(string name)
            : base(name)
        {
        }

        #endregion
    }
}