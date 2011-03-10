using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

using Com.Prerit.Infrastructure.Windsor;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.Windsor
{
    [TestFixture]
    public class WindsorContainerInitializerTests
    {
        #region Tests

        [Test]
        public void Should_Register_IRegistrations_Implementations()
        {
            //arrange
            var container = new WindsorContainer();

            //act
            new WindsorContainerInitializer().Init(container);

            IHandler[] handlers = container.Kernel.GetAssignableHandlers(typeof(object));
            IHandler[] iRegistrationHandlers = container.Kernel.GetHandlers(typeof(IRegistration));

            //assert
            Assert.That(handlers, Is.Not.Empty & Has.Count.GreaterThan(iRegistrationHandlers.Length));
        }

        [Test]
        public void Should_Register_IRegistrations_With_Interfaces()
        {
            //arrange
            var container = new WindsorContainer();

            //act
            new WindsorContainerInitializer().Init(container);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(IRegistration));

            //assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Should_Resolve_All_IRegistrations()
        {
            //arrange
            var container = new WindsorContainer();

            //act
            new WindsorContainerInitializer().Init(container);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(IRegistration));

            IRegistration[] registrations = container.ResolveAll<IRegistration>();

            //assert
            Assert.That(registrations, Is.Not.Empty & Has.Length.EqualTo(handlers.Length));
        }

        #endregion
    }
}