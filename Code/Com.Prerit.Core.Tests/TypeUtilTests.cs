using System;

using NUnit.Framework;

namespace Com.Prerit.Core.Tests
{
    [TestFixture]
    public class TypeUtilTests
    {
        #region Tests

        [Test]
        public void ShouldFailToGetNameFromConst()
        {
            TestDelegate code = () => TypeUtil.GetMemberName<Person>(p => Person.Const);

            Assert.That(code, Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        public void ShouldGetNameFromField()
        {
            Assert.That(TypeUtil.GetMemberName<Person>(p => p.Field), Is.EqualTo("Field"));
        }

        [Test]
        public void ShouldGetNameFromMethodWithNoReturnType()
        {
            Assert.That(TypeUtil.GetMethodName<Person>(p => p.MethodWithNoReturnType()), Is.EqualTo("MethodWithNoReturnType"));
        }

        [Test]
        public void ShouldGetNameFromMethodWithReturnType()
        {
            Assert.That(TypeUtil.GetMethodName<Person>(p => p.MethodWithReturnType()), Is.EqualTo("MethodWithReturnType"));
        }

        [Test]
        public void ShouldGetNameFromProperty()
        {
            Assert.That(TypeUtil.GetMemberName<Person>(p => p.Property), Is.EqualTo("Property"));
        }

        #endregion

        #region Nested Type: Person

        private class Person
        {
            #region Constants

            public const object Const = null;

            #endregion

            #region Fields

            public object Field;

            #endregion

            #region Properties

            public object Property { get; set; }

            #endregion

            #region Methods

            public void MethodWithNoReturnType()
            {
            }

            public object MethodWithReturnType()
            {
                return null;
            }

            #endregion
        }

        #endregion
    }
}