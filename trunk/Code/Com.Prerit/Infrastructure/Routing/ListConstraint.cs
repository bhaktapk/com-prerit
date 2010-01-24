using System;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public class ListConstraint : DashableRouteConstraint
    {
        #region Constructors

        public ListConstraint(params string[] list)
        {
            DashableData = list;
        }

        #endregion

        #region Methods

        public override bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
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
                return DashableData.Any(item => string.Compare(item, (string) value, StringComparison.OrdinalIgnoreCase) == 0);
            }

            return false;
        }

        #endregion
    }
}