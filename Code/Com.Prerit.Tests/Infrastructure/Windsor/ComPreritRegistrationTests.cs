using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Castle.Core;
using Castle.MicroKernel;
using Castle.Windsor;

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
        public void Should_Register_MapCreators_With_Interfaces()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service == typeof(IMapCreator))
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_ModelBinders_With_Interfaces()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service == typeof(IModelBinder))
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_Services_With_A_Name_That_Ends_With_Service()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service.Name.EndsWith("Service"))
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_Services_With_Interfaces()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service.Name.EndsWith("Service") && handler.Service.IsInterface)
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_StartupTasks_With_Interfaces()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service == typeof(IStartupTask))
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_Transient_Controllers()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service.GetInterfaces().Contains(typeof(IController)) &&
                                                              handler.ComponentModel.LifestyleType == LifestyleType.Transient)
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_Transient_Services()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service.Name.EndsWith("Service") &&
                                                              handler.ComponentModel.LifestyleType == LifestyleType.Transient)
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        #endregion
    }
}