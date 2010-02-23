using System;
using System.Web;
using System.Web.Routing;

using Com.Prerit.Domain;
using Com.Prerit.Services;

namespace Com.Prerit.Infrastructure.Routing
{
    public class AlbumPhotoIndexConstraint : IRouteConstraint
    {
        #region Fields

        private readonly IAlbumService _albumService;

        private readonly string _slugParameterName;

        private readonly string _yearParameterName;

        #endregion

        #region Constructors

        public AlbumPhotoIndexConstraint(string yearParameterName, string slugParameterName, IAlbumService albumService)
        {
            if (yearParameterName == null)
            {
                throw new ArgumentNullException("yearParameterName");
            }

            if (slugParameterName == null)
            {
                throw new ArgumentNullException("slugParameterName");
            }

            if (albumService == null)
            {
                throw new ArgumentNullException("albumService");
            }

            _yearParameterName = yearParameterName;
            _slugParameterName = slugParameterName;
            _albumService = albumService;
        }

        #endregion

        #region Methods

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName");
            }

            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            var extractor = new AlbumRouteValueExtractor(values);

            int? photoIndex = extractor.ExtractInt(parameterName);

            string slug = extractor.ExtractString(_slugParameterName);

            int? year = extractor.ExtractInt(_yearParameterName);

            if (photoIndex == null || photoIndex < 0 || slug == null || year == null)
            {
                return false;
            }

            Album album = _albumService.GetAlbum(year.Value, slug);

            if (album == null)
            {
                return false;
            }

            return photoIndex < album.PhotoCount;
        }

        #endregion
    }
}