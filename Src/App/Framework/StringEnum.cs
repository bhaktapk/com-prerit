using System;

namespace Framework
{
	public abstract class StringEnum<T> : IEquatable<T> where T : StringEnum<T>
	{
		private readonly string value;

		public string Value
		{
			get
			{
				return value;
			}
		}

		protected StringEnum(string value)
		{
			this.value = value;
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

		public static implicit operator string(StringEnum<T> stringEnum)
		{
			return stringEnum.Value;
		}
	}
}
