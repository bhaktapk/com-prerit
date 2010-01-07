using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public class SeoRoute : Route
    {
        #region Constructors

        public SeoRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
        }

        public SeoRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        public SeoRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        public SeoRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        #endregion

        #region Methods

        private void DashifyUrlParams(RouteValueDictionary values)
        {
            if (values == null)
            {
                return;
            }

            foreach (string key in GetUrlParamKeysWithSafeValues(values))
            {
                values[key] = GetDashedValue((string) values[key]);
            }
        }

        private static string GetDashedValue(string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return "";
            }

            var seoChars = new List<char>();

            for (int i = 0; i < val.Length; i++)
            {
                if (i != 0 && char.IsUpper(val[i]))
                {
                    seoChars.Add('-');
                }

                seoChars.Add(val[i]);
            }

            return new string(seoChars.ToArray());
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData routeData = base.GetRouteData(httpContext);

            UndashifyUrlParams(routeData);

            return routeData;
        }

        private static string GetUndashedValue(string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return "";
            }

            return val.Replace("-", "");
        }

        private string[] GetUrlParamKeysWithSafeValues(RouteValueDictionary routeValueDictionary)
        {
            var urlParams = new[]
                                {
                                    "action", "controller"
                                };

            return (from key in routeValueDictionary.Keys
                    where urlParams.Contains(key, StringComparer.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(routeValueDictionary[key] as string)
                    select key).ToArray();
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            DashifyUrlParams(values);

            VirtualPathData path = base.GetVirtualPath(requestContext, values);

            LowercaseVirtualPath(path);

            return path;
        }

        private void LowercaseVirtualPath(VirtualPathData path)
        {
            if (path != null)
            {
                path.VirtualPath = path.VirtualPath.ToLowerInvariant();
            }
        }

        private void UndashifyUrlParams(RouteData routeData)
        {
            if (routeData == null || routeData.Values == null)
            {
                return;
            }

            foreach (string key in GetUrlParamKeysWithSafeValues(routeData.Values))
            {
                routeData.Values[key] = GetUndashedValue((string) routeData.Values[key]);
            }
        }

        #endregion
    }
}