using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public abstract class DashableRouteConstraint : IRouteConstraint
    {
        #region Fields

        private IEnumerable<string> _dashableData;

        #endregion

        #region Properties

        public IEnumerable<string> DashableData
        {
            get { return _dashableData; }
            set { _dashableData = value ?? new string[0]; }
        }

        #endregion

        #region Methods

        public abstract bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection);

        #endregion
    }
}