﻿using System.Web.Caching;

using Castle.Windsor;

using Com.Prerit.Infrastructure.Windsor;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.Windsor
{
    [TestFixture]
    public class SystemWebRegistrationTests
    {
        #region Tests

        [Test]
        public void Should_Register_Cache()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new SystemWebRegistration().Register(container.Kernel);

            // assert
            Assert.That((TestDelegate) (() => container.Resolve<Cache>()), Throws.Nothing);
        }

        #endregion
    }
}