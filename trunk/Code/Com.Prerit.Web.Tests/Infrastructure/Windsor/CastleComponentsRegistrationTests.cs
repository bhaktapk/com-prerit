using System.Collections.Generic;
using System.Linq;

using Castle.MicroKernel;
using Castle.Windsor;

using Com.Prerit.Web.Infrastructure.Windsor;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.Windsor
{
    [TestFixture]
    public class CastleComponentsRegistrationTests
    {
        #region Tests

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

        #endregion
    }
}