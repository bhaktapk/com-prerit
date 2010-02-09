using System.Collections.Generic;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public class IgnoreSeoRoute : SeoRoute
    {
        #region Constructors

        public IgnoreSeoRoute(string url, IEnumerable<string> routeParams)
            : base(url, routeParams, new StopRoutingHandler())
        {
        }

        #endregion

        #region Methods

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }

        #endregion
    }
}