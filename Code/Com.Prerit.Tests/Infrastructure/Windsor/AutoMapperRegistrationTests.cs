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

            // act
            container.Register(new AutoMapperRegistration());

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(IConfigurationProvider));

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty & Has.Length.EqualTo(1));
        }

        [Test]
        public void Should_Register_IMappingEngine()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            container.Register(new AutoMapperRegistration());

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(IMappingEngine));
            
            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty & Has.Length.EqualTo(1));
        }

        [Test]
        public void Should_Register_IProfileExpression()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            container.Register(new AutoMapperRegistration());

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(IProfileExpression));

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty & Has.Length.EqualTo(1));
        }

        #endregion
    }
}