using System.Xml.Serialization;

namespace Com.Prerit.Domain
{
    public class Album
    {
        #region Properties

        [XmlIgnore]
        public string DirectoryPath { get; set; }

        [XmlIgnore]
        public int PhotoCount { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        #endregion
    }
}