using System.IO;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace Com.Prerit.Services
{
    public class DiskInputOutputService : IDiskInputOutputService
    {
        #region Methods

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