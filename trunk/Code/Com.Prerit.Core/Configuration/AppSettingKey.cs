namespace Com.Prerit.Core.Configuration
{
    public class AppSettingKey : StringEnum<AppSettingKey>
    {
        #region Fields

        private static readonly AppSettingKey _cachePhotoAlbums = new AppSettingKey("cache-photo-albums");

        #endregion

        #region Properties

        public static AppSettingKey CachePhotoAlbums
        {
            get { return _cachePhotoAlbums; }
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