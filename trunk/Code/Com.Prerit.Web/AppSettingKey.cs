namespace Com.Prerit.Web
{
    public class AppSettingKey : StringEnum<AppSettingKey>
    {
        #region Fields

        private static readonly AppSettingKey _cacheAlbumPhotos = new AppSettingKey("cache-album-photos");

        #endregion

        #region Properties

        public static AppSettingKey CacheAlbumPhotos
        {
            get { return _cacheAlbumPhotos; }
        }

        #endregion

        #region Constructors

        protected AppSettingKey(string name)
            : base(name)
        {
        }

        #endregion
    }
}