using System;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public class AlbumRouteValueExtractor
    {
        #region Fields

        private readonly RouteValueDictionary _values;

        #endregion

        #region Constructors

        public AlbumRouteValueExtractor(RouteValueDictionary values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            _values = values;
        }

        #endregion

        #region Methods

        public bool TryExtractSlug(string slugParameterName, out string slug)
        {
            object slugParameterValue;

            if (_values.TryGetValue(slugParameterName, out slugParameterValue) && slugParameterValue is string)
            {
                slug = (string) slugParameterValue;

                return true;
            }

            slug = null;

            return false;
        }

        public bool TryExtractYear(string yearParameterName, out int year)
        {
            object yearParameterValue;

            if (_values.TryGetValue(yearParameterName, out yearParameterValue))
            {
                if (yearParameterValue is int)
                {
                    year = (int) yearParameterValue;

                    return true;
                }

                if (yearParameterValue is string && int.TryParse((string) yearParameterValue, out year))
                {
                    return true;
                }
            }

            year = 0;

            return false;
        }

        #endregion
    }
}