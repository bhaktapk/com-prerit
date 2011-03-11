using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;

using Castle.Facilities.FactorySupport;
using Castle.MicroKernel;
using Castle.Windsor;

using Com.Prerit.Infrastructure.Windsor;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.Windsor
{
    [TestFixture]
    public class SystemWebRegistrationTests
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
        public void Should_Register_Cache()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new SystemWebRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(Cache));

            // assert
            Assert.That(handlers, Is.Not.Null & Has.Length.EqualTo(1));
        }

        [Test]
        public void Should_Register_HttpContextBase()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new SystemWebRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(HttpContextBase));

            // assert
            Assert.That(handlers, Is.Not.Null & Has.Length.EqualTo(1));
        }

        [Test]
        public void Should_Register_HttpRequestBase()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new SystemWebRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(HttpRequestBase));

            // assert
            Assert.That(handlers, Is.Not.Null & Has.Length.EqualTo(1));
        }

        [Test]
        public void Should_Register_HttpResponseBase()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new SystemWebRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(HttpResponseBase));

            // assert
            Assert.That(handlers, Is.Not.Null & Has.Length.EqualTo(1));
        }

        [Test]
        public void Should_Register_HttpServerUtilityBase()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new SystemWebRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(HttpServerUtilityBase));

            // assert
            Assert.That(handlers, Is.Not.Null & Has.Length.EqualTo(1));
        }

        [Test]
        public void Should_Register_HttpSessionStateBase()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new SystemWebRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(HttpSessionStateBase));

            // assert
            Assert.That(handlers, Is.Not.Null & Has.Length.EqualTo(1));
        }

        [Test]
        public void Should_Register_RouteCollection()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new SystemWebRegistration().Register(container.Kernel);

            IHandler[] handlers = container.Kernel.GetHandlers(typeof(RouteCollection));

            // assert
            Assert.That(handlers, Is.Not.Null & Has.Length.EqualTo(1));
        }

        #endregion
    }
}