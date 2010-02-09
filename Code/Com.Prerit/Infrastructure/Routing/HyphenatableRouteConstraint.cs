using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public abstract class HyphenatableRouteConstraint : IRouteConstraint
    {
        #region Fields

        private IEnumerable<string> _hyphenatableData;

        #endregion

        #region Properties

        public IEnumerable<string> HyphenatableData
        {
            get { return _hyphenatableData; }
            set { _hyphenatableData = value ?? new string[0]; }
        }

        #endregion

        #region Methods

        public abstract bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection);

        #endregion
    }
}