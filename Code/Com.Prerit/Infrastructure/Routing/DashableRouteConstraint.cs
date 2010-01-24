using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public abstract class DashableRouteConstraint : IRouteConstraint
    {
        #region Properties

        public IEnumerable<string> DashableData { get; set; }

        #endregion

        #region Methods

        public abstract bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection);

        #endregion
    }
}