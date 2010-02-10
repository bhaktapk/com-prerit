using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public class ListConstraint : OptimizableRouteConstraint
    {
        #region Constructors

        public ListConstraint(IEnumerable<string> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (list.Count() == 0)
            {
                throw new ArgumentException("Cannot be empty", "list");
            }

            OptimizableData = list;
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
                return OptimizableData.Any(item => string.Compare(item, (string) value, StringComparison.OrdinalIgnoreCase) == 0);
            }

            return false;
        }

        #endregion
    }
}