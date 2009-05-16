namespace Com.Prerit.Core.Configuration
{
    public class AppSettingDefaultValue<T>
    {
        #region Properties

        public static AppSettingDefaultValue<bool> CacheAlbumPhotos { get; private set; }

        public T DefaultValue { get; private set; }

        #endregion

        #region Constructors

        static AppSettingDefaultValue()
        {
            CacheAlbumPhotos = new AppSettingDefaultValue<bool>(true);
        }

        protected AppSettingDefaultValue(T defaultValue)
        {
            DefaultValue = defaultValue;
        }

        #endregion

        #region Operators

        public static implicit operator T(AppSettingDefaultValue<T> appSettingDefaultValue)
        {
            return appSettingDefaultValue.DefaultValue;
        }

        #endregion
    }
}