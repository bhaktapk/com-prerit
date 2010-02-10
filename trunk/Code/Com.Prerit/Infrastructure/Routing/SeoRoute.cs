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

        private static string Deoptimize(string value)
        {
            var chars = new List<char>();

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '-')
                {
                    if (i + 1 < value.Length)
                    {
                        i++;

                        chars.Add(char.ToUpper(value[i]));
                    }
                }
                else if (i == 0)
                {
                    chars.Add(char.ToUpper(value[i]));
                }
                else
                {
                    chars.Add(value[i]);
                }
            }

            return new string(chars.ToArray());
        }

        private void DeoptimizeRouteParamValues(RouteValueDictionary routeValueDictionary)
        {
            var loopableKeys = new List<string>(routeValueDictionary.Keys);

            IEnumerable<string> potentialKeys = loopableKeys.Where(key => RouteParams.Contains(key, StringComparer.OrdinalIgnoreCase));

            foreach (string key in potentialKeys)
            {
                var stringValue = routeValueDictionary[key] as string;
                var hyphenatableValue = routeValueDictionary[key] as HyphenatableRouteConstraint;

                if (!string.IsNullOrEmpty(stringValue) && stringValue.Contains('-'))
                {
                    routeValueDictionary[key] = Deoptimize(stringValue);
                }
                else if (hyphenatableValue != null && hyphenatableValue.HyphenatableData.Any(data => data.Contains('-')))
                {
                    hyphenatableValue.HyphenatableData = from data in hyphenatableValue.HyphenatableData
                                                         select data.Contains('-') ? Deoptimize(data) : data;
                }
            }
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (Defaults != null)
            {
                HyphenateRouteParamValues(Defaults);
            }

            if (Constraints != null)
            {
                HyphenateRouteParamValues(Constraints);
            }

            RouteData routeData = base.GetRouteData(httpContext);

            if (routeData != null && routeData.Values != null)
            {
                DeoptimizeRouteParamValues(routeData.Values);
            }

            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            if (Defaults != null)
            {
                HyphenateRouteParamValues(Defaults);
            }

            if (Constraints != null)
            {
                HyphenateRouteParamValues(Constraints);
            }

            if (values != null)
            {
                HyphenateRouteParamValues(values);
            }

            VirtualPathData path = base.GetVirtualPath(requestContext, values);

            if (path != null)
            {
                LowercaseVirtualPath(path);
            }

            return path;
        }

        private static string Hyphenate(string value)
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

        private void HyphenateRouteParamValues(RouteValueDictionary routeValueDictionary)
        {
            var loopableKeys = new List<string>(routeValueDictionary.Keys);

            IEnumerable<string> potentialKeys = loopableKeys.Where(key => RouteParams.Contains(key, StringComparer.OrdinalIgnoreCase));

            foreach (string key in potentialKeys)
            {
                var stringValue = routeValueDictionary[key] as string;
                var hyphenatableValue = routeValueDictionary[key] as HyphenatableRouteConstraint;

                if (!string.IsNullOrEmpty(stringValue) && !stringValue.Contains('-'))
                {
                    routeValueDictionary[key] = Hyphenate(stringValue);
                }
                else if (hyphenatableValue != null && hyphenatableValue.HyphenatableData.Any(data => !data.Contains('-')))
                {
                    hyphenatableValue.HyphenatableData = from data in hyphenatableValue.HyphenatableData
                                                         select !data.Contains('-') ? Hyphenate(data) : data;
                }
            }
        }

        private void LowercaseVirtualPath(VirtualPathData path)
        {
            path.VirtualPath = path.VirtualPath.ToLower();
        }

        #endregion
    }
}