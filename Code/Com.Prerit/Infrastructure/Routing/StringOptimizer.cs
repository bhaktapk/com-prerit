using System;
using System.Linq;

namespace Com.Prerit.Infrastructure.Routing
{
    public class StringOptimizer : RouteValueOptimizer
    {
        #region Methods

        public override object DeoptimizeRouteValue(object value)
        {
            var stringValue = value as string;

            if (stringValue == null)
            {
                throw new ArgumentException(string.Format("Cannot be casted to {0} or is null", typeof(string).FullName), "value");
            }

            if (!string.IsNullOrEmpty(stringValue) && stringValue.Contains('-'))
            {
                return DeoptimizeCore(stringValue);
            }

            return value;
        }

        public override object OptimizeRouteValue(object value)
        {
            var stringValue = value as string;

            if (stringValue == null)
            {
                throw new ArgumentException(string.Format("Cannot be casted to {0} or is null", typeof(string).FullName), "value");
            }

            if (!string.IsNullOrEmpty(stringValue) && !stringValue.Contains('-'))
            {
                return OptimizeCore(stringValue);
            }

            return value;
        }

        #endregion
    }
}