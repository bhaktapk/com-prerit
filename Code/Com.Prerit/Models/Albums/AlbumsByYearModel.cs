using System.Collections.Generic;

using Com.Prerit.Domain;

namespace Com.Prerit.Models.Albums
{
    public class AlbumsByYearModel
    {
        #region Properties

        public IEnumerable<Album> Albums { get; set; }

        public int Year { get; set; }

        #endregion
    }
}