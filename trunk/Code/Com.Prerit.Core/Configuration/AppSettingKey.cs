namespace Com.Prerit.Core.Configuration
{
    public class AppSettingKey : StringEnum<AppSettingKey>
    {
        #region Properties

        public static AppSettingKey CachePhotoAlbums { get; private set; }

        #endregion

        #region Constructors

        static AppSettingKey()
        {
            CachePhotoAlbums = new AppSettingKey("cache-photo-albums");
        }

        protected AppSettingKey(string name)
            : base(name)
        {
        }

        #endregion
    }
}