using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Castle.Core;
using Castle.Facilities.FactorySupport;
using Castle.MicroKernel;
using Castle.Windsor;

using Com.Prerit.Infrastructure.Routing;
using Com.Prerit.Infrastructure.StartupTasks;
using Com.Prerit.Infrastructure.Windsor;
using Com.Prerit.MapCreators;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.Windsor
{
    [TestFixture]
    public class ComPreritRegistrationTests
    {
        #region Tests

        [Test]
        public void Should_Add_FactorySupportFacility()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            container.Register(new AutoMapperRegistration());

            IEnumerable<IFacility> facilities = from facility in container.Kernel.GetFacilities()
                                                where facility.GetType() == typeof(FactorySupportFacility)
                                                select facility;

            // assert
            Assert.That(facilities, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_Controllers()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new ComPreritRegistration().Register(container.Kernel);

            IEnumerable<IHandler> handlers = container.Kernel.GetAssignableHandlers(typeof(IController));

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_Controllers_That_Are_Not_T4MVC_Generated()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new ComPreritRegistration().Register(container.Kernel);

            IEnumerable<IHandler> handlers = from handler in container.Kernel.GetAssignableHandlers(typeof(IController))
                                             where !handler.Service.Name.StartsWith("T4MVC_", StringComparison.InvariantCultureIgnoreCase)
                                             select handler;

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_Controllers_That_Are_Transient()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new ComPreritRegistration().Register(container.Kernel);

            IEnumerable<IHandler> handlers = from handler in container.Kernel.GetAssignableHandlers(typeof(IController))
                                             where handler.ComponentModel.LifestyleType == LifestyleType.Transient
                                             select handler;

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_IMapCreators()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new ComPreritRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(IMapCreator));

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_IModelBinder()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new ComPreritRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(IModelBinder));

            // assert
            Assert.That(handlers, Is.Not.Null & Has.Length.EqualTo(1));
        }

        [Test]
        public void Should_Register_IRouteValueOptimizers()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new ComPreritRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(IRouteValueOptimizer));

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_IStartupTasks()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new ComPreritRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(IStartupTask));

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_Services_That_Are_Transient()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new ComPreritRegistration().Register(container.Kernel);

            IEnumerable<IHandler> handlers = from handler in container.Kernel.GetAssignableHandlers(typeof(object))
                                             where handler.Service.Name.EndsWith("Service") && handler.ComponentModel.LifestyleType == LifestyleType.Transient
                                             select handler;

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_Services_With_A_Name_That_Ends_With_Service()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new ComPreritRegistration().Register(container.Kernel);

            IEnumerable<IHandler> handlers = from handler in container.Kernel.GetAssignableHandlers(typeof(object))
                                             where handler.Service.Name.EndsWith("Service")
                                             select handler;

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_Services_With_Interfaces()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new ComPreritRegistration().Register(container.Kernel);

            IEnumerable<IHandler> handlers = from handler in container.Kernel.GetAssignableHandlers(typeof(object))
                                             where handler.Service.Name.EndsWith("Service") && handler.Service.IsInterface
                                             select handler;

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        #endregion
    }
}