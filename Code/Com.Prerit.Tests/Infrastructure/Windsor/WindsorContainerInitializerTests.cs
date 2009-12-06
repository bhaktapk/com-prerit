﻿using Castle.MicroKernel.Registration;
using Castle.Windsor;

using Com.Prerit.Infrastructure.Windsor;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.Windsor
{
    [TestFixture]
    public class WindsorContainerInitializerTests
    {
        #region Tests

        [Test]
        public void Should_Initialize_Windsor_Container()
        {
            IWindsorContainer container = WindsorContainerInitializer.Init();

            Assert.That(container.ResolveAll<IRegistration>(), Is.Not.Empty);
        }

        #endregion
    }
}