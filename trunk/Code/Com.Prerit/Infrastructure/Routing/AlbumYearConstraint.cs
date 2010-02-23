using System;
using System.Linq;
using System.Web;
using System.Web.Routing;

using Com.Prerit.Services;

namespace Com.Prerit.Infrastructure.Routing
{
    public class AlbumYearConstraint : IRouteConstraint
    {
        #region Fields

        private readonly IAlbumService _albumService;

        #endregion

        #region Constructors

        public AlbumYearConstraint(IAlbumService albumService)
        {
            if (albumService == null)
            {
                throw new ArgumentNullException("albumService");
            }

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

            int? year = extractor.ExtractInt(parameterName);

            if (year == null)
            {
                return false;
            }

            return _albumService.GetAlbumYears().Contains(year.Value);
        }

        #endregion
    }
}