using System;

using NUnit.Framework;

namespace Com.Prerit.Core.Tests
{
    [TestFixture]
    public class TypeUtilTests
    {
        #region Tests

        [Test]
        public void Should_Fail_To_Get_Name_From_Const()
        {
            TestDelegate code = () => TypeUtil.GetMemberName<Person>(p => Person.Const);

            Assert.That(code, Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        public void Should_Get_Name_From_Field()
        {
            Assert.That(TypeUtil.GetMemberName<Person>(p => p.Field), Is.EqualTo("Field"));
        }

        [Test]
        public void Should_Get_Name_From_Method_With_No_Return_Type()
        {
            Assert.That(TypeUtil.GetMethodName<Person>(p => p.MethodWithNoReturnType()), Is.EqualTo("MethodWithNoReturnType"));
        }

        [Test]
        public void Should_Get_Name_From_Method_With_Return_Type()
        {
            Assert.That(TypeUtil.GetMethodName<Person>(p => p.MethodWithReturnType()), Is.EqualTo("MethodWithReturnType"));
        }

        [Test]
        public void Should_Get_Name_From_Property()
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

            public readonly object Field = new object();

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