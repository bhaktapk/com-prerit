using System.Web.Routing;

using Com.Prerit.Web.Infrastructure.StartupTasks;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class RegisterRoutesStartupTaskTests
    {
        #region Setup/Teardown

        [TearDown]
        public void TearDown()
        {
            StartupTaskRunner.Reset();
        }

        #endregion

        #region Tests

        [Test]
        public void Should_Add_Routes()
        {
            new RegisterRoutesStartupTask().Execute();

            Assert.That(RouteTable.Routes, Is.Not.Empty);
        }

        [Test]
        public void Should_Reset_Routes()
        {
            var task = new RegisterRoutesStartupTask();

            task.Execute();
            task.Reset();

            Assert.That(RouteTable.Routes, Is.Empty);
        }

        #endregion
    }
}