using System.Web.Mvc;

using Castle.Windsor;

using Com.Prerit.Web.Infrastructure.StartupTasks;
using Com.Prerit.Web.Infrastructure.Windsor;

using CommonServiceLocator.WindsorAdapter;

using Microsoft.Practices.ServiceLocation;

using Moq;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class StartupTaskRunnerTests
    {
        #region Tests

        [Test]
        public void Should_Configure_ControllerFactory()
        {
            // arrange
            var container = new Mock<IWindsorContainer>();

            container.Setup(c => c.ResolveAll(typeof(IStartupTask))).Returns(new IStartupTask[] { });

            // act
            StartupTaskRunner.Run(container.Object);

            // assert
            Assert.That(ControllerBuilder.Current.GetControllerFactory(), Is.TypeOf<WindsorControllerFactory>());
        }

        [Test]
        public void Should_Configure_ServiceLocator()
        {
            // arrange
            var container = new Mock<IWindsorContainer>();

            container.Setup(c => c.ResolveAll(typeof(IStartupTask))).Returns(new IStartupTask[] { });

            // act
            StartupTaskRunner.Run(container.Object);

            // assert
            Assert.That(ServiceLocator.Current, Is.TypeOf<WindsorServiceLocator>());
        }

        [Test]
        public void Should_Execute_StartupTasks()
        {
            // arrange
            var container = new Mock<IWindsorContainer>();
            var startupTask = new Mock<IStartupTask>();

            container.Setup(c => c.ResolveAll(typeof(IStartupTask))).Returns(new[] { startupTask.Object, startupTask.Object });

            // act
            StartupTaskRunner.Run(container.Object);

            // assert
            startupTask.Verify(task => task.Execute(), Times.Exactly(2));
        }

        [Test]
        public void Should_Reset_ControllerFactory()
        {
            // arrange
            var serviceLocator = new Mock<IServiceLocator>();

            serviceLocator.Setup(sl => sl.GetAllInstances<IStartupTask>()).Returns(new IStartupTask[] { });

            ServiceLocator.SetLocatorProvider(() => serviceLocator.Object);

            // act
            StartupTaskRunner.Reset();

            // assert
            Assert.That(ControllerBuilder.Current.GetControllerFactory(), Is.TypeOf<DefaultControllerFactory>());
        }

        [Test]
        public void Should_Reset_ServiceLocator()
        {
            // arrange
            var serviceLocator = new Mock<IServiceLocator>();

            serviceLocator.Setup(sl => sl.GetAllInstances<IStartupTask>()).Returns(new IStartupTask[] { });

            ServiceLocator.SetLocatorProvider(() => serviceLocator.Object);

            // act
            StartupTaskRunner.Reset();

            // assert
            Assert.That(ServiceLocator.Current, Is.Null);
        }

        [Test]
        public void Should_Reset_StartupTasks()
        {
            // arrange
            var serviceLocator = new Mock<IServiceLocator>();
            var startupTask = new Mock<IStartupTask>();

            serviceLocator.Setup(sl => sl.GetAllInstances<IStartupTask>()).Returns(new[] { startupTask.Object, startupTask.Object });

            ServiceLocator.SetLocatorProvider(() => serviceLocator.Object);

            // act
            StartupTaskRunner.Reset();

            // assert
            startupTask.Verify(task => task.Reset(), Times.Exactly(2));
        }

        [Test]
        public void Should_Run_With_Auto_Configuration()
        {
            // act
            StartupTaskRunner.Run();
        }

        #endregion
    }
}