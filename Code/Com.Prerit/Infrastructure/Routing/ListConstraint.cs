using System;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public class ListConstraint : IRouteConstraint
    {
        #region Fields

        private readonly string[] _list;

        #endregion

        #region Constructors

        public ListConstraint(params string[] list)
        {
            _list = list;
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

            if (values.TryGetValue(parameterName, out value) && value is string)
            {
                return _list.Any(item => string.Compare(item, (string) value, StringComparison.OrdinalIgnoreCase) == 0);
            }

            return false;
        }

        #endregion
    }
}