using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public static class RouteCollectionExtensions
    {
        #region Methods

        public static void IgnoreSeoRoute(this RouteCollection routes, string url, IEnumerable<string> routeParams)
        {
            routes.IgnoreSeoRoute(url, routeParams, null);
        }

        public static void IgnoreSeoRoute(this RouteCollection routes, string url, IEnumerable<string> routeParams, object constraints)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            if (routeParams == null)
            {
                throw new ArgumentNullException("routeParams");
            }

            if (routeParams.Count() == 0)
            {
                throw new ArgumentException("Must have at least one route param", "routeParams");
            }

            routes.Add(new IgnoreSeoRoute(url, routeParams)
                           {
                               Constraints = new RouteValueDictionary(constraints),
                           });
        }

        public static void MapSeoRoute(this RouteCollection routes, string name, string url, IEnumerable<string> routeParams)
        {
            routes.MapSeoRoute(name, url, routeParams, null, null, null);
        }

        public static void MapSeoRoute(this RouteCollection routes, string name, string url, IEnumerable<string> routeParams, object defaults)
        {
            routes.MapSeoRoute(name, url, routeParams, defaults, null, null);
        }

        public static void MapSeoRoute(this RouteCollection routes, string name, string url, IEnumerable<string> routeParams, string[] namespaces)
        {
            routes.MapSeoRoute(name, url, routeParams, null, null, namespaces);
        }

        public static void MapSeoRoute(this RouteCollection routes,
                                       string name,
                                       string url,
                                       IEnumerable<string> routeParams,
                                       object defaults,
                                       object constraints)
        {
            routes.MapSeoRoute(name, url, routeParams, defaults, constraints, null);
        }

        public static void MapSeoRoute(this RouteCollection routes,
                                       string name,
                                       string url,
                                       IEnumerable<string> routeParams,
                                       object defaults,
                                       string[] namespaces)
        {
            routes.MapSeoRoute(name, url, routeParams, defaults, null, namespaces);
        }

        public static void MapSeoRoute(this RouteCollection routes,
                                       string name,
                                       string url,
                                       IEnumerable<string> routeParams,
                                       object defaults,
                                       object constraints,
                                       string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            if (routeParams == null)
            {
                throw new ArgumentNullException("routeParams");
            }

            if (routeParams.Count() == 0)
            {
                throw new ArgumentException("Must have at least one route param", "routeParams");
            }

            var route = new SeoRoute(url, routeParams, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new MvcRouteHandler());

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