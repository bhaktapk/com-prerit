using System;

using Microsoft.Practices.ServiceLocation;

namespace Com.Prerit.Infrastructure.Routing
{
    public static class RouteValueOptimizerFactory
    {
        #region Methods

        public static IRouteValueOptimizer Create(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return ServiceLocator.Current.GetInstance<IRouteValueOptimizer>(type.FullName);
        }

        #endregion
    }
}