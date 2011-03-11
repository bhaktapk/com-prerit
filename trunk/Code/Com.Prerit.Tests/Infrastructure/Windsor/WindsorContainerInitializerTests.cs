using System.Collections.Generic;
using System.Linq;

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

            IEnumerable<IHandler> handlers = from handler in container.Kernel.GetAssignableHandlers(typeof(object))
                                             where handler.Service != typeof(IRegistration)
                                             select handler;

            //assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
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
            Assert.That(registrations, Is.Not.Null & Has.Length.EqualTo(handlers.Length));
        }

        #endregion
    }
}