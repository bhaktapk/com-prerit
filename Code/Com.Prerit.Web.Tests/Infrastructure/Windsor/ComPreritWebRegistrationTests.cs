using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Castle.Core;
using Castle.MicroKernel;
using Castle.Windsor;

using Com.Prerit.Web.Infrastructure.MapCreators;
using Com.Prerit.Web.Infrastructure.StartupTasks;
using Com.Prerit.Web.Infrastructure.Windsor;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.Windsor
{
    [TestFixture]
    public class ComPreritWebRegistrationTests
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
            new ComPreritWebRegistration().Register(container.Kernel);

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
            new ComPreritWebRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Empty);
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
            new ComPreritWebRegistration().Register(container.Kernel);

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
            new ComPreritWebRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Empty);
            Assert.That(handlers, Has.All.Matches((IHandler h) => h.ComponentModel.LifestyleType == LifestyleType.Transient));
        }

        #endregion
    }
}