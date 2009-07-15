using System;
using System.Linq.Expressions;

using Castle.Components.Validator;

using Com.Prerit.Core;

using NUnit.Framework;

namespace Com.Prerit.Domain.Tests
{
    [TestFixture]
    public class EmailTests
    {
        #region Tests

        [Test]
        public void ShouldContainValidateEmailAttributeOnFromEmailAddressProperty()
        {
            object[] attributes = GetAttributes(e => e.FromEmailAddress, typeof(ValidateEmailAttribute));

            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void ShouldContainValidateEmailAttributeOnToEmailAddressProperty()
        {
            object[] attributes = GetAttributes(e => e.ToEmailAddress, typeof(ValidateEmailAttribute));

            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void ShouldContainValidateNonEmptyAttributeOnFromEmailAddressProperty()
        {
            object[] attributes = GetAttributes(e => e.FromEmailAddress, typeof(ValidateNonEmptyAttribute));

            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void ShouldContainValidateNonEmptyAttributeOnMessageProperty()
        {
            object[] attributes = GetAttributes(e => e.Message, typeof(ValidateNonEmptyAttribute));

            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void ShouldContainValidateNonEmptyAttributeOnSubjectProperty()
        {
            object[] attributes = GetAttributes(e => e.Subject, typeof(ValidateNonEmptyAttribute));

            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void ShouldContainValidateNonEmptyAttributeOnToEmailAddressProperty()
        {
            object[] attributes = GetAttributes(e => e.ToEmailAddress, typeof(ValidateNonEmptyAttribute));

            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        #endregion

        #region Methods

        private object[] GetAttributes(Expression<Func<Email, object>> member, Type attributeType)
        {
            return typeof(Email).GetProperty(TypeUtil.GetMemberName(member)).GetCustomAttributes(attributeType, false);
        }

        #endregion
    }
}