using System.Collections.Generic;

namespace Com.Prerit.Domain
{
    public class Album
    {
        #region Properties

        public string DirectoryPath { get; set; }

        public IEnumerable<Photo> Photos { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        #endregion
    }
}