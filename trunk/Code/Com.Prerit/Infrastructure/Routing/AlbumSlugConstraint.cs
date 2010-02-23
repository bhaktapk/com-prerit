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

        private readonly string _yearParameterName;

        #endregion

        #region Constructors

        public AlbumSlugConstraint(string yearParameterName, IAlbumService albumService)
        {
            if (yearParameterName == null)
            {
                throw new ArgumentNullException("yearParameterName");
            }

            if (albumService == null)
            {
                throw new ArgumentNullException("albumService");
            }

            _yearParameterName = yearParameterName;
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

            string slug;

            var extractor = new AlbumRouteValueExtractor(values);

            if (extractor.TryExtractSlug(parameterName, out slug))
            {
                int year;

                if (extractor.TryExtractYear(_yearParameterName, out year))
                {
                    return _albumService.GetAlbumSlugs(year).Contains(slug);
                }
            }

            return false;
        }

        #endregion
    }
}