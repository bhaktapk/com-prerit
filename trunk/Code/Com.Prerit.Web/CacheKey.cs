namespace Com.Prerit.Web
{
    public class CacheKey : StringEnum<CacheKey>
    {
        #region Fields

        private static readonly CacheKey _albumsGroupedByAlbumYear = new CacheKey("AlbumsGroupedByAlbumYear");

        #endregion

        #region Properties

        public static CacheKey AlbumsGroupedByAlbumYear
        {
            get { return _albumsGroupedByAlbumYear; }
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