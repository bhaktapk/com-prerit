using System.Collections.Generic;

using AutoMapper;

using Castle.MicroKernel;
using Castle.Windsor;

using Com.Prerit.Infrastructure.Windsor;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.Windsor
{
    [TestFixture]
    public class AutoMapperRegistrationTests
    {
        #region Tests

        [Test]
        public void Should_Not_Use_Static_Mapper_Class()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            container.Register(new AutoMapperRegistration());

            var mappingEngine = container.Resolve<IMappingEngine>();

            // assert
            Assert.That(mappingEngine, Is.Not.EqualTo(Mapper.Engine));
        }

        [Test]
        public void Should_Register_IConfigurationProvider()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service == typeof(IConfigurationProvider))
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            container.Register(new AutoMapperRegistration());

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_IMappingEngine()
        {
            // arrange
            var container = new WindsorContainer();

            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service == typeof(IMappingEngine))
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            container.Register(new AutoMapperRegistration());

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Register_IProfileExpression()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service == typeof(IProfileExpression))
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            container.Register(new AutoMapperRegistration());

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        #endregion
    }
}