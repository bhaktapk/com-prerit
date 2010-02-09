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

        private IEnumerable<string> _routeParams;

        #endregion

        #region Properties

        public IEnumerable<string> RouteParams
        {
            get { return _routeParams; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (value.Count() == 0)
                {
                    throw new ArgumentException("Must have at least one route param", "value");
                }

                _routeParams = value;
            }
        }

        #endregion

        #region Constructors

        public SeoRoute(string url, IEnumerable<string> routeParams, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
            RouteParams = routeParams;
        }

        public SeoRoute(string url, IEnumerable<string> routeParams, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
            RouteParams = routeParams;
        }

        public SeoRoute(string url, IEnumerable<string> routeParams, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
            RouteParams = routeParams;
        }

        public SeoRoute(string url,
                        IEnumerable<string> routeParams,
                        RouteValueDictionary defaults,
                        RouteValueDictionary constraints,
                        RouteValueDictionary dataTokens,
                        IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            RouteParams = routeParams;
        }

        #endregion

        #region Methods

        private void DashifyRouteParamValues(RouteValueDictionary routeValueDictionary)
        {
            foreach (string key in GetDashableRouteParams(routeValueDictionary))
            {
                if (routeValueDictionary[key] is string)
                {
                    routeValueDictionary[key] = GetDashedValue((string) routeValueDictionary[key]);
                }
                else if (routeValueDictionary[key] is DashableRouteConstraint)
                {
                    var dashable = (DashableRouteConstraint) routeValueDictionary[key];

                    dashable.DashableData = from data in dashable.DashableData
                                            select !data.Contains('-') ? GetDashedValue(data) : data;
                }
            }
        }

        private string[] GetDashableRouteParams(RouteValueDictionary routeValueDictionary)
        {
            IEnumerable<KeyValuePair<string, object>> potentialRouteParams = from kvp in routeValueDictionary
                                                                             where RouteParams.Contains(kvp.Key, StringComparer.OrdinalIgnoreCase)
                                                                             select kvp;

            IEnumerable<string> stringRouteParams = from kvp in potentialRouteParams
                                                    where !string.IsNullOrEmpty(kvp.Value as string) && !((string) kvp.Value).Contains('-')
                                                    select kvp.Key;

            IEnumerable<string> idashableRouteParams = from kvp in potentialRouteParams
                                                       where
                                                           kvp.Value as DashableRouteConstraint != null &&
                                                           ((DashableRouteConstraint) kvp.Value).DashableData.Any(data => !data.Contains('-'))
                                                       select kvp.Key;

            return stringRouteParams.Concat(idashableRouteParams).ToArray();
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
            IEnumerable<KeyValuePair<string, object>> potentialRouteParams = from kvp in routeValueDictionary
                                                                             where RouteParams.Contains(kvp.Key, StringComparer.OrdinalIgnoreCase)
                                                                             select kvp;

            IEnumerable<string> stringRouteParams = from kvp in potentialRouteParams
                                                    where !string.IsNullOrEmpty(kvp.Value as string) && ((string) kvp.Value).Contains('-')
                                                    select kvp.Key;

            IEnumerable<string> idashableRouteParams = from kvp in potentialRouteParams
                                                       where
                                                           kvp.Value as DashableRouteConstraint != null &&
                                                           ((DashableRouteConstraint) kvp.Value).DashableData.Any(data => data.Contains('-'))
                                                       select kvp.Key;

            return stringRouteParams.Concat(idashableRouteParams).ToArray();
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
            path.VirtualPath = path.VirtualPath.ToLower();
        }

        private void UndashifyRouteParamValues(RouteValueDictionary routeValueDictionary)
        {
            foreach (string key in GetUndashableRouteParams(routeValueDictionary))
            {
                if (routeValueDictionary[key] is string)
                {
                    routeValueDictionary[key] = GetUndashedValue((string) routeValueDictionary[key]);
                }
                else if (routeValueDictionary[key] is DashableRouteConstraint)
                {
                    var dashable = (DashableRouteConstraint) routeValueDictionary[key];

                    dashable.DashableData = from data in dashable.DashableData
                                            select data.Contains('-') ? GetUndashedValue(data) : data;
                }
            }
        }

        #endregion
    }
}