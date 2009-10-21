﻿using System.Web.Mvc;

using Castle.Components.Validator;

using Com.Prerit.Web.Infrastructure.ModelBinders;
using Com.Prerit.Web.Infrastructure.StartupTasks;

using Microsoft.Practices.ServiceLocation;

using Moq;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class RegisterDefaultModelBinderStartupTaskTests
    {
        #region Tests

        [Test]
        public void Should_Reset_Binder()
        {
            // arrange
            ModelBinders.Binders.DefaultBinder = null;

            // act
            new RegisterDefaultModelBinderStartupTask().Reset();

            // assert
            Assert.That(ModelBinders.Binders.DefaultBinder, Is.InstanceOf<DefaultModelBinder>());
        }

        [Test]
        public void Should_Set_Default_Binder()
        {
            // arrange
            var serviceLocator = new Mock<IServiceLocator>();
            var runner = new Mock<IValidatorRunner>();
            var simpleValidatingModelBinder = new Mock<SimpleValidatingModelBinder>(runner.Object);

            serviceLocator.Setup(sl => sl.GetInstance<SimpleValidatingModelBinder>()).Returns(simpleValidatingModelBinder.Object);

            ServiceLocator.SetLocatorProvider(() => serviceLocator.Object);

            // act
            new RegisterDefaultModelBinderStartupTask().Execute();

            // assert
            Assert.That(ModelBinders.Binders.DefaultBinder, Is.InstanceOf<SimpleValidatingModelBinder>());

            // teardown
            ModelBinders.Binders.DefaultBinder = new DefaultModelBinder();
            ServiceLocator.SetLocatorProvider(() => null);
        }

        #endregion
    }
}