using System;

using Com.Prerit.Core;

using NUnit.Framework;

namespace Com.Prerit.Tests.Core
{
    [TestFixture]
    public class StringEnumTests
    {
        #region Tests

        [Test]
        public void Should_Be_Equal_To_Itself_When_ToStringed()
        {
            var a = new HelperStringEnum("a");

            Assert.That(a.ToString(), Is.EqualTo(a.Value));
        }

        [Test]
        public void Should_Be_Equal_When_Casted_To_Object()
        {
            var a1 = new HelperStringEnum("a");
            var a2 = new HelperStringEnum("a");

            Assert.That(((Object) a1).Equals(a2), Is.True);
        }

        [Test]
        public void Should_Be_Equal_When_Compared()
        {
            var a1 = new HelperStringEnum("a");
            var a2 = new HelperStringEnum("a");

            Assert.That(a1.CompareTo(a2), Is.EqualTo(0));
        }

        [Test]
        public void Should_Be_Equal_When_Hashed()
        {
            var a1 = new HelperStringEnum("a");
            var a2 = new HelperStringEnum("a");

            Assert.That(a1.GetHashCode(), Is.EqualTo(a2.GetHashCode()));
        }

        [Test]
        public void Should_Be_Greater_Than_When_Compared_To_Null()
        {
            var a = new HelperStringEnum("a");

            Assert.That(a.CompareTo(null), Is.GreaterThan(0));
        }

        [Test]
        public void Should_Be_Greater_When_Compared()
        {
            var a = new HelperStringEnum("a");
            var b = new HelperStringEnum("b");

            Assert.That(b.CompareTo(a), Is.GreaterThan(0));
        }

        [Test]
        public void Should_Be_Less_Than_When_Compared()
        {
            var a = new HelperStringEnum("a");
            var b = new HelperStringEnum("b");

            Assert.That(a.CompareTo(b), Is.LessThan(0));
        }

        [Test]
        public void Should_Fail_When_Constructed_With_Null_Value()
        {
            TestDelegate code = () => new HelperStringEnum(null);

            Assert.That(code, Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void Should_Not_Be_Equal_When_Casted_To_Object()
        {
            var a = new HelperStringEnum("a");
            var b = new HelperStringEnum("b");

            Assert.That(((Object) a).Equals(b), Is.False);
        }

        [Test]
        public void Should_Not_Be_Equal_When_Casted_To_Object_And_Tested_With_Null()
        {
            var a = new HelperStringEnum("a");
            var b = (HelperStringEnum) null;

            Assert.That(((Object) a).Equals(b), Is.False);
        }

        #endregion

        #region Nested Type: HelperStringEnum

        private class HelperStringEnum : StringEnum<HelperStringEnum>
        {
            #region Constructors

            public HelperStringEnum(string value)
                : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}