using System;
using System.Linq;
using System.Web;
using System.Web.Routing;

using Com.Prerit.Services;

namespace Com.Prerit.Infrastructure.Routing
{
    public class AlbumSlugConstraint : IRouteConstraint
    {
        #region Fields

        private readonly IAlbumService _albumService;

        private readonly string _yearRouteParam;

        #endregion

        #region Constructors

        public AlbumSlugConstraint(string yearRouteParam, IAlbumService albumService)
        {
            if (yearRouteParam == null)
            {
                throw new ArgumentNullException("yearRouteParam");
            }

            if (albumService == null)
            {
                throw new ArgumentNullException("albumService");
            }

            _yearRouteParam = yearRouteParam;
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

            object parameterValue;

            if (!values.TryGetValue(parameterName, out parameterValue) || !(parameterValue is string))
            {
                return false;
            }

            object yearParamValue;

            if (!values.TryGetValue(_yearRouteParam, out yearParamValue))
            {
                return false;
            }

            int? year = TryGetYear(yearParamValue);

            if (year == null)
            {
                return false;
            }

            return _albumService.GetAlbumSlugs(year.Value).Contains((string) parameterValue);
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