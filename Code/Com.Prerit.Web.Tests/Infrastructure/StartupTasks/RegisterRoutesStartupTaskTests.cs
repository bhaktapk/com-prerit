﻿using System.Web.Mvc;
using System.Web.Routing;

using Com.Prerit.Web.Controllers;
using Com.Prerit.Web.Infrastructure.StartupTasks;

using Microsoft.Practices.ServiceLocation;

using Moq;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class RegisterRoutesStartupTaskTests
    {
        #region Tests

        [Test]
        public void Should_Add_Routes()
        {
            // act
            new RegisterRoutesStartupTask().Execute();

            // assert
            Assert.That(RouteTable.Routes, Is.Not.Empty);

            // teardown
            RouteTable.Routes.Clear();
        }

        [Test]
        public void Should_Reset_Routes()
        {
            // arrange
            RouteTable.Routes.MapRoute("", "");

            // act
            new RegisterRoutesStartupTask().Reset();

            // assert
            Assert.That(RouteTable.Routes, Is.Empty);
        }

        #endregion
    }
}