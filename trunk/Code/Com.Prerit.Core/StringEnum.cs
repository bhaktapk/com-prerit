using System;

namespace Com.Prerit.Core
{
    public abstract class StringEnum<T> : IEquatable<T>, IComparable<T> where T : StringEnum<T>
    {
        #region Properties

        public string Value { get; private set; }

        #endregion

        #region Constructors

        protected StringEnum(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            Value = value;
        }

        #endregion

        #region Methods

        public int CompareTo(T other)
        {
            if (other == null)
            {
                return 1;
            }

            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            return string.Compare(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        public bool Equals(T other)
        {
            return other != null && (ReferenceEquals(this, other) || Value == other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        #endregion

        #region Operators

        public static implicit operator string(StringEnum<T> stringEnum)
        {
            return stringEnum.Value;
        }

        #endregion
    }
}