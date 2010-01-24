using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Com.Prerit.Infrastructure.Routing
{
    public class SeoRoute : Route
    {
        #region Fields

        private static readonly string[] RouteParams = new[]
                                                           {
                                                               "action", "controller"
                                                           };

        #endregion

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

        private void DashifyRouteParamValues(RouteValueDictionary routeValueDictionary)
        {
            foreach (string key in GetDashableRouteParams(routeValueDictionary))
            {
                routeValueDictionary[key] = GetDashedValue((string) routeValueDictionary[key]);
            }
        }

        private string[] GetDashableRouteParams(RouteValueDictionary routeValueDictionary)
        {
            return (from key in routeValueDictionary.Keys
                    where
                        RouteParams.Contains(key, StringComparer.OrdinalIgnoreCase) && !string.IsNullOrEmpty(routeValueDictionary[key] as string) &&
                        !((string) routeValueDictionary[key]).Contains('-')
                    select key).ToArray();
        }

        private static string GetDashedValue(string value)
        {
            var seoChars = new List<char>();

            for (int i = 0; i < value.Length; i++)
            {
                if (i != 0 && char.IsUpper(value[i]))
                {
                    seoChars.Add('-');
                }

                seoChars.Add(value[i]);
            }

            return new string(seoChars.ToArray());
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (Defaults != null)
            {
                DashifyRouteParamValues(Defaults);
            }

            if (Constraints != null)
            {
                DashifyRouteParamValues(Constraints);
            }

            RouteData routeData = base.GetRouteData(httpContext);

            if (routeData != null && routeData.Values != null)
            {
                UndashifyRouteParamValues(routeData.Values);
            }

            return routeData;
        }

        private string[] GetUndashableRouteParams(RouteValueDictionary routeValueDictionary)
        {
            return (from key in routeValueDictionary.Keys
                    where
                        RouteParams.Contains(key, StringComparer.OrdinalIgnoreCase) && !string.IsNullOrEmpty(routeValueDictionary[key] as string) &&
                        ((string) routeValueDictionary[key]).Contains('-')
                    select key).ToArray();
        }

        private static string GetUndashedValue(string value)
        {
            return value.Replace("-", "");
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            if (Defaults != null)
            {
                DashifyRouteParamValues(Defaults);
            }

            if (Constraints != null)
            {
                DashifyRouteParamValues(Constraints);
            }

            if (values != null)
            {
                DashifyRouteParamValues(values);
            }

            VirtualPathData path = base.GetVirtualPath(requestContext, values);

            if (path != null)
            {
                LowercaseVirtualPath(path);
            }

            return path;
        }

        private void LowercaseVirtualPath(VirtualPathData path)
        {
            path.VirtualPath = path.VirtualPath.ToLowerInvariant();
        }

        private void UndashifyRouteParamValues(RouteValueDictionary routeValueDictionary)
        {
            foreach (string key in GetUndashableRouteParams(routeValueDictionary))
            {
                routeValueDictionary[key] = GetUndashedValue((string) routeValueDictionary[key]);
            }
        }

        #endregion
    }
}