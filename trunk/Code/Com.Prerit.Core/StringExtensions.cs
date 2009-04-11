using System;

namespace Com.Prerit.Core
{
    public static class StringExtensions
    {
        #region Methods

        public static string ToLowerFirstLetter(this string s)
        {
            if (s == null)
            {
                throw new NullReferenceException();
            }

            if (s.Length != 0)
            {
                char[] chars = s.ToCharArray();

                if (!char.IsLower(chars[0]))
                {
                    chars[0] = char.ToLowerInvariant(chars[0]);

                    return new string(chars);
                }
            }

            return s;
        }

        #endregion
    }
}