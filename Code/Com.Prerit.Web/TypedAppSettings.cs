using System.Configuration;

namespace Com.Prerit.Web
{
    public static class TypedAppSettings
    {
        #region Properties

        public static bool CacheAlbumPhotos
        {
            get
            {
                bool result;

                if (!bool.TryParse(ConfigurationManager.AppSettings[AppSettingKey.CacheAlbumPhotos], out result))
                {
                    const bool cacheAlbumPhotosDefaultValue = true;

                    result = cacheAlbumPhotosDefaultValue;
                }

                return result;
            }
        }

        #endregion
    }
}