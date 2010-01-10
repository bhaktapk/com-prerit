using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public static class RouteCollectionExtensions
    {
        #region Methods

        public static void MapSeoRoute(this RouteCollection routes, string name, string url)
        {
            routes.MapSeoRoute(name, url, null, null);
        }

        public static void MapSeoRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            routes.MapSeoRoute(name, url, defaults, null);
        }

        public static void MapSeoRoute(this RouteCollection routes, string name, string url, string[] namespaces)
        {
            routes.MapSeoRoute(name, url, null, null, namespaces);
        }

        public static void MapSeoRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            routes.MapSeoRoute(name, url, defaults, constraints, null);
        }

        public static void MapSeoRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
        {
            routes.MapSeoRoute(name, url, defaults, null, namespaces);
        }

        public static void MapSeoRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            var route = new SeoRoute(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new MvcRouteHandler());

            if (namespaces != null && namespaces.Length > 0)
            {
                route.DataTokens = new RouteValueDictionary();
                route.DataTokens["Namespaces"] = namespaces;
            }

            if (string.IsNullOrEmpty(name))
            {
                routes.Add(route);
            }
            else
            {
                routes.Add(name, route);
            }
        }

        #endregion
    }
}