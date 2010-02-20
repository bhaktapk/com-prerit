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

        private readonly object _optimizeConstraintsSyncRoot = new object();

        private bool _optimizedConstraints;

        private bool _optimizedDefaults;

        private readonly object _optimizeDefaultsSyncRoot = new object();

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

        private void DeoptimizeRouteValues(RouteValueDictionary routeValueDictionary)
        {
            IEnumerable<string> potentialKeys = from kvp in routeValueDictionary
                                                where RouteParams.Contains(kvp.Key, StringComparer.OrdinalIgnoreCase) && kvp.Value != null
                                                select kvp.Key;

            var loopableKeys = new List<string>(potentialKeys);

            foreach (string key in loopableKeys)
            {
                object routeValue = routeValueDictionary[key];

                IRouteValueOptimizer optimizer = RouteValueOptimizerFactory.Create(routeValue.GetType());

                routeValueDictionary[key] = optimizer.DeoptimizeRouteValue(routeValue);
            }
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            OptimizeDefaults();

            OptimizeConstraints();

            RouteData routeData = base.GetRouteData(httpContext);

            if (routeData != null && routeData.Values != null)
            {
                DeoptimizeRouteValues(routeData.Values);
            }

            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            OptimizeDefaults();

            OptimizeConstraints();

            RouteValueDictionary clonedValues = values != null ? new RouteValueDictionary(values) : null;

            if (clonedValues != null)
            {
                OptimizeRouteValues(clonedValues);
            }

            VirtualPathData path = base.GetVirtualPath(requestContext, clonedValues);

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

        private void OptimizeConstraints()
        {
            if (Constraints == null)
            {
                return;
            }

            if (_optimizedConstraints)
            {
                return;
            }

            lock (_optimizeConstraintsSyncRoot)
            {
                if (_optimizedConstraints)
                {
                    return;
                }

                OptimizeRouteValues(Constraints);

                _optimizedConstraints = true;
            }
        }

        private void OptimizeDefaults()
        {
            if (Defaults == null)
            {
                return;
            }

            if (_optimizedDefaults)
            {
                return;
            }

            lock (_optimizeDefaultsSyncRoot)
            {
                if (_optimizedDefaults)
                {
                    return;
                }

                OptimizeRouteValues(Defaults);

                _optimizedDefaults = true;
            }
        }

        private void OptimizeRouteValues(RouteValueDictionary routeValueDictionary)
        {
            IEnumerable<string> potentialKeys = from kvp in routeValueDictionary
                                                where RouteParams.Contains(kvp.Key, StringComparer.OrdinalIgnoreCase) && kvp.Value != null
                                                select kvp.Key;

            var loopableKeys = new List<string>(potentialKeys);

            foreach (string key in loopableKeys)
            {
                object routeValue = routeValueDictionary[key];

                IRouteValueOptimizer optimizer = RouteValueOptimizerFactory.Create(routeValue.GetType());

                routeValueDictionary[key] = optimizer.OptimizeRouteValue(routeValue);
            }
        }

        #endregion
    }
}