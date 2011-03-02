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
            // arrange
            var a1 = new HelperStringEnum("a");

            // act
            string a2 = a1.ToString();

            // assert
            Assert.That(a2, Is.EqualTo(a1.Value));
        }

        [Test]
        public void Should_Be_Equal_When_Casted_To_Object()
        {
            // arrange
            object a1 = new HelperStringEnum("a");
            var a2 = new HelperStringEnum("a");

            // act
            bool isEquatable = a1.Equals(a2);

            // assert
            Assert.That(isEquatable, Is.True);
        }

        [Test]
        public void Should_Be_Equal_When_Compared()
        {
            // arrange
            var a1 = new HelperStringEnum("a");
            var a2 = new HelperStringEnum("a");

            // act
            int comparisonResult = a1.CompareTo(a2);

            // assert
            Assert.That(comparisonResult, Is.EqualTo(0));
        }

        [Test]
        public void Should_Be_Equal_When_Hashed()
        {
            // arrange
            var a1 = new HelperStringEnum("a");
            var a2 = new HelperStringEnum("a");

            // act
            int hashCode = a1.GetHashCode();

            // assert
            Assert.That(hashCode, Is.EqualTo(a2.GetHashCode()));
        }

        [Test]
        public void Should_Be_Greater_Than_When_Compared_To_Null()
        {
            // arrange
            var a = new HelperStringEnum("a");

            // act
            int comparisonResult = a.CompareTo(null);

            // assert
            Assert.That(comparisonResult, Is.GreaterThan(0));
        }

        [Test]
        public void Should_Be_Greater_When_Compared()
        {
            // arrange
            var a = new HelperStringEnum("a");
            var b = new HelperStringEnum("b");

            // act
            int comparisonResult = b.CompareTo(a);

            // assert
            Assert.That(comparisonResult, Is.GreaterThan(0));
        }

        [Test]
        public void Should_Be_Less_Than_When_Compared()
        {
            // arrange
            var a = new HelperStringEnum("a");
            var b = new HelperStringEnum("b");

            // act
            int comparisonResult = a.CompareTo(b);

            // assert
            Assert.That(comparisonResult, Is.LessThan(0));
        }

        [Test]
        public void Should_Fail_When_Constructed_With_Null_Value()
        {
            // arrange

            // act
            TestDelegate act = () => new HelperStringEnum(null);

            // assert
            Assert.That(act, Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void Should_Not_Be_Equal_When_Casted_To_Object()
        {
            // arrange
            object a = new HelperStringEnum("a");
            var b = new HelperStringEnum("b");

            // act
            bool isEquatable = a.Equals(b);

            // assert
            Assert.That(isEquatable, Is.False);
        }

        [Test]
        public void Should_Not_Be_Equal_When_Casted_To_Object_And_Tested_With_Null()
        {
            object a = new HelperStringEnum("a");
            var b = (HelperStringEnum) null;

            // act
            bool isEquatable = a.Equals(b);

            // assert
            Assert.That(isEquatable, Is.False);
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