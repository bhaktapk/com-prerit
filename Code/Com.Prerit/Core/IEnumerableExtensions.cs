using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Prerit.Core
{
    public static class IEnumerableExtensions
    {
        #region Methods

        public static IEnumerable<T> ExecuteQuery<T>(this IEnumerable<T> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.ToArray();
        }

        #endregion
    }
}