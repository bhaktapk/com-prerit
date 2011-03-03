using AutoMapper;

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
        public void Should_Register_Transient_IConfiguration()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            container.Register(new AutoMapperRegistration());

            var configuration1 = container.Resolve<IConfiguration>();
            var configuration2 = container.Resolve<IConfiguration>();

            // assert
            Assert.That(configuration1, Is.Not.EqualTo(configuration2));
        }

        [Test]
        public void Should_Register_Transient_IMappingEngine()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            container.Register(new AutoMapperRegistration());

            var mappingEngine1 = container.Resolve<IMappingEngine>();
            var mappingEngine2 = container.Resolve<IMappingEngine>();

            // assert
            Assert.That(mappingEngine1, Is.Not.EqualTo(mappingEngine2));
        }

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

        #endregion
    }
}