using System.Collections.Generic;

namespace Com.Prerit.Services
{
    public interface IDiskInputOutputService
    {
        #region Methods

        bool FileExists(string filePath);

        IEnumerable<string> GetDirectories(string path);

        T LoadXmlFile<T>(string filePath);

        string MapPath(string virtualPath);

        void SaveXmlFile<T>(string filePath, T obj);

        #endregion
    }
}