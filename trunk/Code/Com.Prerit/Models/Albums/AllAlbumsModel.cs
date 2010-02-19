using System.Collections.Generic;
using System.Linq;

using Com.Prerit.Domain;

namespace Com.Prerit.Models.Albums
{
    public class AllAlbumsModel
    {
        #region Properties

        public IEnumerable<IGrouping<int, Album>> GroupedAlbums { get; set; }

        #endregion
    }
}