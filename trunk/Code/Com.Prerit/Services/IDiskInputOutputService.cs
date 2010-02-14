namespace Com.Prerit.Services
{
    public interface IDiskInputOutputService
    {
        #region Methods

        T LoadXmlFile<T>(string filePath);

        string MapPath(string virtualPath);

        void SaveXmlFile<T>(string filePath, T obj);

        #endregion
    }
}