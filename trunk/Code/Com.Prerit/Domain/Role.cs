using System.Collections.Generic;
using System.Xml.Serialization;

namespace Com.Prerit.Domain
{
    public class Role
    {
        #region Properties

        [XmlArrayItem(typeof(string), ElementName = "Id")]
        public List<string> Ids { get; set; }

        public string Name { get; set; }

        #endregion
    }
}