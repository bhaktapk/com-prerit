using System.Collections.Generic;
using System.Linq;

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
        public void Should_Register_Services_With_Interfaces()
        {
            // arrange
            var container = new WindsorContainer();

            // act
            new CastleComponentsRegistration().Register(container.Kernel);

            IEnumerable<IHandler> handlers = from handler in container.Kernel.GetAssignableHandlers(typeof(object))
                                             where handler.Service.IsInterface
                                             select handler;

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        #endregion
    }
}