using System;

using NUnit.Framework;

namespace Com.Prerit.Core.Tests
{
    [TestFixture]
    public class StringEnumTests
    {
        #region Tests

        [Test]
        public void ShouldBeEqualToItselfWhenToStringed()
        {
            var a = new HelperStringEnum("a");

            Assert.That(a.ToString(), Is.EqualTo(a.Value));
        }

        [Test]
        public void ShouldBeEqualWhenCastedToObject()
        {
            var a1 = new HelperStringEnum("a");
            var a2 = new HelperStringEnum("a");

            Assert.That(((Object) a1).Equals(a2), Is.True);
        }

        [Test]
        public void ShouldBeEqualWhenCompared()
        {
            var a1 = new HelperStringEnum("a");
            var a2 = new HelperStringEnum("a");

            Assert.That(a1.CompareTo(a2), Is.EqualTo(0));
        }

        [Test]
        public void ShouldBeEqualWhenHashed()
        {
            var a1 = new HelperStringEnum("a");
            var a2 = new HelperStringEnum("a");

            Assert.That(a1.GetHashCode(), Is.EqualTo(a2.GetHashCode()));
        }

        [Test]
        public void ShouldBeGreaterThanWhenComparedToNull()
        {
            var a = new HelperStringEnum("a");

            Assert.That(a.CompareTo(null), Is.GreaterThan(0));
        }

        [Test]
        public void ShouldBeGreaterWhenCompared()
        {
            var a = new HelperStringEnum("a");
            var b = new HelperStringEnum("b");

            Assert.That(b.CompareTo(a), Is.GreaterThan(0));
        }

        [Test]
        public void ShouldBeLessThanWhenCompared()
        {
            var a = new HelperStringEnum("a");
            var b = new HelperStringEnum("b");

            Assert.That(a.CompareTo(b), Is.LessThan(0));
        }

        [Test]
        public void ShouldFailWhenConstructedWithNullValue()
        {
            TestDelegate code = () => new HelperStringEnum(null);

            Assert.That(code, Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void ShouldNotBeEqualWhenCastedToObject()
        {
            var a = new HelperStringEnum("a");
            var b = new HelperStringEnum("b");

            Assert.That(((Object) a).Equals(b), Is.False);
        }

        [Test]
        public void ShouldNotBeEqualWhenCastedToObjectAndTestedWithNull()
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