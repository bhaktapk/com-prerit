using System.Web.Mvc;
using System.Web.Routing;

using Com.Prerit.Infrastructure.StartupTasks;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class RegisterRoutesStartupTaskTests
    {
        #region Tests

        [Test]
        public void Should_Add_Routes()
        {
            // arrange
            RouteTable.Routes.Clear();

            // act
            new RegisterRoutesStartupTask().Execute();

            // assert
            Assert.That(RouteTable.Routes, Is.Not.Empty);
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