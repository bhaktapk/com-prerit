using System.Collections.Generic;

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
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) =>
                                                      {
                                                          if (handler.Service.IsInterface)
                                                          {
                                                              handlers.Add(handler);
                                                          }
                                                      };

            // act
            new CastleComponentsRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Is.Not.Null.And.Not.Empty);
        }

        #endregion
    }
}