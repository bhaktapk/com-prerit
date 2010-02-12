using System;
using System.Collections.Generic;
using System.Linq;

using Com.Prerit.Domain;

namespace Com.Prerit.Services
{
    public class AlbumService : IAlbumService
    {
        #region Methods

        public IEnumerable<Album> GetAlbums()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Album> GetAlbums(int year)
        {
            IEnumerable<Album> albums = GetAlbums();

            return albums.Where(album => album.Year == year);
        }

        public IEnumerable<IGrouping<int, Album>> GetAlbumsGroupedByYear()
        {
            IEnumerable<Album> albums = GetAlbums();

            return albums.GroupBy(album => album.Year);
        }

        public IEnumerable<string> GetAlbumTitles(int year)
        {
            IEnumerable<Album> albums = GetAlbums(year);

            return albums.Select(album => album.Title);
        }

        public IEnumerable<int> GetAlbumYears()
        {
            IEnumerable<Album> albums = GetAlbums();

            return albums.Select(album => album.Year).Distinct();
        }

        #endregion
    }
}