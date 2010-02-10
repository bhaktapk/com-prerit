using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public abstract class OptimizableRouteConstraint : IRouteConstraint
    {
        #region Fields

        private IEnumerable<string> _optimizableData;

        #endregion

        #region Properties

        public IEnumerable<string> OptimizableData
        {
            get { return _optimizableData; }
            set { _optimizableData = value ?? new string[0]; }
        }

        #endregion

        #region Methods

        public abstract bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection);

        #endregion
    }
}