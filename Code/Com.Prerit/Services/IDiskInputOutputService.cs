using System.Collections.Generic;
using System.IO;

namespace Com.Prerit.Services
{
    public interface IDiskInputOutputService
    {
        #region Properties

        IImageEditorService ImageEditor { get; }

        #endregion

        #region Methods

        bool FileExists(string filePath);

        IEnumerable<string> GetDirectories(string path);

        IEnumerable<string> GetFiles(string path);

        IEnumerable<string> GetFiles(string path, string searchPattern);

        IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption);

        T LoadXmlFile<T>(string filePath);

        string MapPath(string virtualPath);

        void SaveXmlFile<T>(string filePath, T obj);

        #endregion
    }
}