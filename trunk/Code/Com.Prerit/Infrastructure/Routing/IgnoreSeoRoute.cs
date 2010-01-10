using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public class IgnoreSeoRoute : SeoRoute
    {
        public IgnoreSeoRoute(string url)
            : base(url, new StopRoutingHandler())
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}