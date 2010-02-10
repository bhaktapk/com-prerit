using System;
using System.Linq;

namespace Com.Prerit.Infrastructure.Routing
{
    public class RouteConstraintOptimizer : RouteValueOptimizer
    {
        #region Methods

        public override object DeoptimizeRouteValue(object value)
        {
            var constraint = value as OptimizableRouteConstraint;

            if (constraint == null)
            {
                throw new ArgumentException(string.Format("Cannot be casted to {0} or is null", typeof(OptimizableRouteConstraint).FullName), "value");
            }

            if (constraint.OptimizableData.Any(data => data.Contains('-')))
            {
                constraint.OptimizableData = from data in constraint.OptimizableData
                                             select data.Contains('-') ? DeoptimizeCore(data) : data;
            }

            return value;
        }

        public override object OptimizeRouteValue(object value)
        {
            var constraint = value as OptimizableRouteConstraint;

            if (constraint == null)
            {
                throw new ArgumentException(string.Format("Cannot be casted to {0} or is null", typeof(OptimizableRouteConstraint).FullName), "value");
            }

            if (constraint.OptimizableData.Any(data => !data.Contains('-')))
            {
                constraint.OptimizableData = from data in constraint.OptimizableData
                                             select !data.Contains('-') ? OptimizeCore(data) : data;
            }

            return value;
        }

        #endregion
    }
}