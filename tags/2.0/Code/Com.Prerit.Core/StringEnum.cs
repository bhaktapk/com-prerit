using System;

namespace Com.Prerit.Core
{
    public abstract class StringEnum<T> : IEquatable<T> where T : StringEnum<T>
    {
        #region Properties

        public string Value
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        protected StringEnum(string value)
        {
            Value = value;
        }

        #endregion

        #region Methods

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