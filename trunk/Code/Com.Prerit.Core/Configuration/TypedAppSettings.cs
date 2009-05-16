using System.Configuration;

namespace Com.Prerit.Core.Configuration
{
    public static class TypedAppSettings
    {
        #region Properties

        public static bool CacheAlbumPhotos
        {
            get
            {
                bool result;

                if (bool.TryParse(ConfigurationManager.AppSettings[AppSettingKey.CachePhotoAlbums], out result))
                {
                    return result;
                }

                return AppSettingDefaultValue<bool>.CacheAlbumPhotos;
            }
        }

        #endregion
    }
}