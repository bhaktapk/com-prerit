using System;
using System.Linq.Expressions;

using Castle.Components.Validator;

using Com.Prerit.Core;
using Com.Prerit.Domain;

using NUnit.Framework;

namespace Com.Prerit.Tests.Domain
{
    [TestFixture]
    public class EmailTests
    {
        #region Tests

        [Test]
        public void Should_Contain_ValidateEmailAttribute_On_FromEmailAddress_Property()
        {
            // arrange
            object[] attributes = GetAttributes<ValidateEmailAttribute>(e => e.FromEmailAddress);

            // act

            // assert
            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void Should_Contain_ValidateEmailAttribute_On_ToEmailAddress_Property()
        {
            // arrange
            object[] attributes = GetAttributes<ValidateEmailAttribute>(e => e.ToEmailAddress);

            // act

            // assert
            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void Should_Contain_ValidateNonEmptyAttribute_On_FromEmailAddress_Property()
        {
            // arrange
            object[] attributes = GetAttributes<ValidateNonEmptyAttribute>(e => e.FromEmailAddress);

            // act

            // assert
            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void Should_Contain_ValidateNonEmptyAttribute_On_Message_Property()
        {
            // arrange
            object[] attributes = GetAttributes<ValidateNonEmptyAttribute>(e => e.Message);

            // act

            // assert
            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void Should_Contain_ValidateNonEmptyAttribute_On_Subject_Property()
        {
            // arrange
            object[] attributes = GetAttributes<ValidateNonEmptyAttribute>(e => e.Subject);

            // act

            // assert
            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void Should_Contain_ValidateNonEmptyAttribute_On_ToEmailAddress_Property()
        {
            // arrange
            object[] attributes = GetAttributes<ValidateNonEmptyAttribute>(e => e.ToEmailAddress);

            // act

            // assert
            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        #endregion

        #region Methods

        private object[] GetAttributes<T>(Expression<Func<Email, object>> member)
        {
            return Reflect<Email>.GetProperty(member).GetCustomAttributes(typeof(T), false);
        }

        #endregion
    }
}