using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Castle.Core;
using Castle.MicroKernel;
using Castle.Windsor;

using Com.Prerit.Infrastructure.MapCreators;
using Com.Prerit.Infrastructure.StartupTasks;
using Com.Prerit.Infrastructure.Windsor;
using Com.Prerit.Services;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.Windsor
{
    [TestFixture]
    public class ComPreritRegistrationTests
    {
        #region Tests

        [Test]
        public void Should_Register_MapCreators()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service.GetInterfaces().Contains(typeof(IMapCreator)))
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Empty);
        }

        [Test]
        public void Should_Register_ModelBinders()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service.GetInterfaces().Contains(typeof(IModelBinder)))
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Empty);
        }

        [Test]
        public void Should_Register_Services_With_A_Name_That_Ends_With_Service()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) => handlers.Add(handler);

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Has.Some.Matches((IHandler h) => h.ComponentModel.Implementation.Name.EndsWith("Service")));
        }

        [Test]
        public void Should_Register_Services_With_Interfaces()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) => handlers.Add(handler);

            // act
            new CastleComponentsRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Has.Some.Matches((IHandler h) => h.ComponentModel.Implementation.GetInterfaces().Contains(h.Service)));
        }

        [Test]
        public void Should_Register_StartupTasks()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service.GetInterfaces().Contains(typeof(IStartupTask)))
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Empty);
        }

        [Test]
        public void Should_Register_Transient_Controllers()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service.GetInterfaces().Contains(typeof(IController)))
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Empty);
            Assert.That(handlers, Has.All.Matches((IHandler h) => h.ComponentModel.LifestyleType == LifestyleType.Transient));
        }

        [Test]
        public void Should_Register_Transient_Services()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          Type[] interfaces = handler.Service.GetInterfaces();

                                                          if (interfaces.Length > 0 && interfaces[0].Name.EndsWith("Service"))
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new ComPreritRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Has.All.Matches((IHandler h) => h.ComponentModel.LifestyleType == LifestyleType.Transient));
        }

        #endregion
    }
}