using Castle.Components.Validator;
using Castle.MicroKernel;
using Castle.Windsor;

using Com.Prerit.Infrastructure.Windsor;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.Windsor
{
    [TestFixture]
    public class CastleComponentsRegistrationTests
    {
        #region Tests

        [Test]
        public void Should_Register_IValidatorRegistry()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new CastleComponentsRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(IValidatorRegistry));

            // assert
            Assert.That(handlers, Is.Not.Null & Has.Length.EqualTo(1));
        }

        [Test]
        public void Should_Register_IValidatorRunner()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new CastleComponentsRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(IValidatorRunner));

            // assert
            Assert.That(handlers, Is.Not.Null & Has.Length.EqualTo(1));
        }

        #endregion
    }
}