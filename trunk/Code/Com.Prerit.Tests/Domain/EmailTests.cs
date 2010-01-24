﻿using System;
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
            object[] attributes = GetAttributes(e => e.FromEmailAddress, typeof(ValidateEmailAttribute));

            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void Should_Contain_ValidateEmailAttribute_On_ToEmailAddress_Property()
        {
            object[] attributes = GetAttributes(e => e.ToEmailAddress, typeof(ValidateEmailAttribute));

            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void Should_Contain_ValidateNonEmptyAttribute_On_FromEmailAddress_Property()
        {
            object[] attributes = GetAttributes(e => e.FromEmailAddress, typeof(ValidateNonEmptyAttribute));

            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void Should_Contain_ValidateNonEmptyAttribute_On_Message_Property()
        {
            object[] attributes = GetAttributes(e => e.Message, typeof(ValidateNonEmptyAttribute));

            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void Should_Contain_ValidateNonEmptyAttribute_On_Subject_Property()
        {
            object[] attributes = GetAttributes(e => e.Subject, typeof(ValidateNonEmptyAttribute));

            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        [Test]
        public void Should_Contain_ValidateNonEmptyAttribute_On_ToEmailAddress_Property()
        {
            object[] attributes = GetAttributes(e => e.ToEmailAddress, typeof(ValidateNonEmptyAttribute));

            Assert.That(attributes.Length, Is.EqualTo(1));
        }

        #endregion

        #region Methods

        private object[] GetAttributes(Expression<Func<Email, object>> member, Type attributeType)
        {
            return Reflect<Email>.GetProperty(member).GetCustomAttributes(attributeType, false);
        }

        #endregion
    }
}