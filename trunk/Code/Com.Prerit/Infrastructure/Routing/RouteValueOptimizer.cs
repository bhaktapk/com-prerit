using System.Collections.Generic;

namespace Com.Prerit.Infrastructure.Routing
{
    public abstract class RouteValueOptimizer : IRouteValueOptimizer
    {
        #region Methods

        protected string DeoptimizeCore(string value)
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

        public abstract object DeoptimizeRouteValue(object value);

        protected string OptimizeCore(string value)
        {
            var chars = new List<char>();

            for (int i = 0; i < value.Length; i++)
            {
                if (i > 0 && char.IsUpper(value[i]))
                {
                    chars.Add('-');
                }

                if (char.IsUpper(value[i]))
                {
                    chars.Add(char.ToLower(value[i]));
                }
                else
                {
                    chars.Add(value[i]);
                }
            }

            return new string(chars.ToArray());
        }

        public abstract object OptimizeRouteValue(object value);

        #endregion
    }
}