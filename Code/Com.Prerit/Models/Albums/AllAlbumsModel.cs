using System.Collections.Generic;

using Com.Prerit.Domain;

namespace Com.Prerit.Models.Albums
{
    public class AllAlbumsModel
    {
        #region Fields

        private IEnumerable<Album> _albums;

        #endregion

        #region Properties

        public IEnumerable<Album> Albums
        {
            get { return _albums; }
            set { _albums = value ?? new Album[0]; }
        }

        #endregion
    }
}