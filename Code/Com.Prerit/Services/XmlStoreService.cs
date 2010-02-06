using System.IO;
using System.Xml.Serialization;

namespace Com.Prerit.Services
{
    public class XmlStoreService : IXmlStoreService
    {
        #region Methods

        public T Load<T>(string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StreamReader(filePath))
            {
                return (T) serializer.Deserialize(reader);
            }
        }

        public void Save<T>(string filePath, T obj)
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