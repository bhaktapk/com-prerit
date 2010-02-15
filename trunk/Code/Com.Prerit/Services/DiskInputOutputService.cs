using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace Com.Prerit.Services
{
    public class DiskInputOutputService : IDiskInputOutputService
    {
        #region Methods

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public IEnumerable<string> GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        public IEnumerable<string> GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public IEnumerable<string> GetFiles(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern);
        }

        public IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetFiles(path, searchPattern, searchOption);
        }

        public T LoadXmlFile<T>(string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StreamReader(filePath))
            {
                return (T) serializer.Deserialize(reader);
            }
        }

        public string MapPath(string virtualPath)
        {
            return HostingEnvironment.MapPath(virtualPath);
        }

        public void SaveXmlFile<T>(string filePath, T obj)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, obj);
            }
        }

        #endregion
    }
}