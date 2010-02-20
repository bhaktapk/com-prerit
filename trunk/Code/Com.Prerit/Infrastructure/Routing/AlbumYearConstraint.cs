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

            object value;

            if (!values.TryGetValue(parameterName, out value))
            {
                return false;
            }

            int? year = TryGetYear(value);

            if (year == null)
            {
                return false;
            }

            return _albumService.GetAlbumYears().Contains(year.Value);
        }

        private int? TryGetYear(object value)
        {
            int parsedYear;

            if (value is int)
            {
                return (int) value;
            }

            if (value is string && int.TryParse((string) value, out parsedYear))
            {
                return parsedYear;
            }

            return null;
        }

        #endregion
    }
}