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

        public int? ExtractInt(string parameterName)
        {
            object parameterValue;

            int typedParameterValue;

            if (_values.TryGetValue(parameterName, out parameterValue))
            {
                if (parameterValue is int)
                {
                    return (int) parameterValue;
                }

                if (parameterValue is string && int.TryParse((string) parameterValue, out typedParameterValue))
                {
                    return typedParameterValue;
                }
            }

            return null;
        }

        public string ExtractString(string parameterName)
        {
            object parameterValue;

            if (_values.TryGetValue(parameterName, out parameterValue) && parameterValue is string)
            {
                return (string) parameterValue;
            }

            return null;
        }

        #endregion
    }
}