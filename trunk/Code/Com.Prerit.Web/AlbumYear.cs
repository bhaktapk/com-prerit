using System;

namespace Com.Prerit.Web
{
    public class AlbumYear
    {
        #region Properties

        public Album[] Albums
        {
            get;
            private set;
        }

        public int Year
        {
            get;
            private set;
        }

        public string VirtualPath
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public AlbumYear(int year, string virtualPath, Album[] albums)
        {
            if (virtualPath == null)
            {
                throw new ArgumentNullException("virtualPath");
            }

            if (albums == null)
            {
                throw new ArgumentNullException("albums");
            }

            Array.ForEach(albums, album =>
            {
                if (album.AlbumYear != year)
                {
                    throw new ArgumentException(string.Format("Albums contains the album {0} which was not taken in the year {1}", album.AlbumName, year),
                                                "albums");
                }
            });

            Year = year;
            VirtualPath = virtualPath;
            Albums = albums;
        }

        #endregion
    }
}