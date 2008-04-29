using System;

namespace Com.Prerit.Web
{
    public abstract class StringEnum<T> : IEquatable<T> where T : StringEnum<T>
    {
        #region Fields

        private readonly string value;

        #endregion

        #region Properties

        public string Value
        {
            get { return value; }
        }

        #endregion

        #region Constructors

        protected StringEnum(string value)
        {
            this.value = value;
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