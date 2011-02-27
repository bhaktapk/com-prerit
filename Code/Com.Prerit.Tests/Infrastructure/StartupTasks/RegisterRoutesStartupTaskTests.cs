using System.Web.Mvc;
using System.Web.Routing;

using Com.Prerit.Infrastructure.StartupTasks;
using Com.Prerit.Services;

using Moq;

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
            var albumService = new Mock<IAlbumService>();

            RouteTable.Routes.Clear();

            // act
            new RegisterRoutesStartupTask(albumService.Object).Execute();

            // assert
            Assert.That(RouteTable.Routes, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Reset_Routes()
        {
            // arrange
            var albumService = new Mock<IAlbumService>();

            RouteTable.Routes.MapRoute("", "");

            // act
            new RegisterRoutesStartupTask(albumService.Object).Reset();

            // assert
            Assert.That(RouteTable.Routes, Is.Empty);
        }

        #endregion
    }
}