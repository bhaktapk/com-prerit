using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Com.Prerit.Core
{
    public static class TypeUtil
    {
        #region Methods

        public static PropertyInfo GetPropertyInfo<T, TResult>(Expression<Func<T, TResult>> p)
        {
            return GetPropertyInfo((LambdaExpression) p);
        }

        private static PropertyInfo GetPropertyInfo(LambdaExpression p)
        {
            MemberExpression memberExpression;

            if (p.Body is UnaryExpression)
            {
                var ue = (UnaryExpression) p.Body;

                memberExpression = (MemberExpression) ue.Operand;
            }
            else
            {
                memberExpression = (MemberExpression) p.Body;
            }

            return (PropertyInfo) (memberExpression).Member;
        }

        public static string GetPropertyName<T, TResult>(Expression<Func<T, TResult>> p)
        {
            return GetPropertyInfo((LambdaExpression) p).Name;
        }

        #endregion
    }
}