using System.Web.Mvc;

using Castle.MicroKernel;
using Castle.Windsor;

using Com.Prerit.Infrastructure.StartupTasks;

using CommonServiceLocator.WindsorAdapter;

using Microsoft.Practices.ServiceLocation;

using Moq;

using MvcContrib.Castle;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.StartupTasks
{
    [TestFixture]
    public class StartupTaskRunnerTests
    {
        #region Tests

        [Test]
        public void Should_Configure_ControllerFactory()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            StartupTaskRunner.Run(container);

            // assert
            Assert.That(ControllerBuilder.Current.GetControllerFactory(), Is.TypeOf<WindsorControllerFactory>());
        }

        [Test]
        public void Should_Configure_ServiceLocator()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            StartupTaskRunner.Run(container);

            // assert
            Assert.That(ServiceLocator.Current, Is.TypeOf<WindsorServiceLocator>());
        }

        [Test]
        public void Should_Execute_StartupTasks()
        {
            // arrange
            var container = new Mock<IWindsorContainer>();
            var kernel = new Mock<IKernel>();
            var startupTask = new Mock<IStartupTask>();

            container.Setup(c => c.ResolveAll(typeof(IStartupTask))).Returns(new[] { startupTask.Object, startupTask.Object });
            container.SetupGet(c => c.Kernel).Returns(kernel.Object);

            kernel.Setup(k => k.GetAssignableHandlers(typeof(object))).Returns(new IHandler[0]);

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

        #endregion
    }
}