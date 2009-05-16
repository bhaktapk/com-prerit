using System;
using System.Linq.Expressions;

namespace Com.Prerit.Core
{
    public static class TypeUtil
    {
        #region Methods

        public static string GetMemberName<T>(Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new ArgumentException("Expression is not a MemberExpression", "expression");
            }

            return memberExpression.Member.Name;
        }

        public static string GetMethodName<T>(Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            var methodCallExpression = expression.Body as MethodCallExpression;

            if (methodCallExpression == null)
            {
                throw new ArgumentException("Expression is not a MethodCallExpression", "expression");
            }

            return methodCallExpression.Method.Name;
        }

        public static string GetMethodName<T>(Expression<Action<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            var methodCallExpression = expression.Body as MethodCallExpression;

            if (methodCallExpression == null)
            {
                throw new ArgumentException("Expression is not a MethodCallExpression", "expression");
            }

            return methodCallExpression.Method.Name;
        }

        #endregion
    }
}