using System.Web.Mvc;

using Com.Prerit.Web.Infrastructure.StartupTasks;
using Com.Prerit.Web.Infrastructure.Windsor;

using CommonServiceLocator.WindsorAdapter;

using Microsoft.Practices.ServiceLocation;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class StartupTaskRunnerTests
    {
        #region Setup/Teardown

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            typeof(StartupTaskRunner).TypeInitializer.Invoke(null, new object[0]);
        }

        #endregion

        #region Tests

        [Test]
        public void Should_Configure_ControllerFactory()
        {
            Assert.That(ControllerBuilder.Current.GetControllerFactory(), Is.TypeOf<WindsorControllerFactory>());
        }

        [Test]
        public void Should_Configure_ServiceLocator()
        {
            Assert.That(ServiceLocator.Current, Is.TypeOf<WindsorServiceLocator>());
        }

        [Test]
        public void Should_Run_StartupTasks()
        {
            Assert.That(() => StartupTaskRunner.Run(), Throws.Nothing);
        }

        #endregion
    }
}