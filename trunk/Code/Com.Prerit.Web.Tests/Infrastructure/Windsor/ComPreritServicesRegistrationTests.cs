using System.Collections.Generic;
using System.Linq;

using Castle.Core;
using Castle.MicroKernel;
using Castle.Windsor;

using Com.Prerit.Services;
using Com.Prerit.Web.Infrastructure.Windsor;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.Windsor
{
    [TestFixture]
    public class ComPreritServicesRegistrationTests
    {
        #region Tests

        [Test]
        public void Should_Register_Services_With_A_Name_That_Ends_With_Service()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) => handlers.Add(handler);

            // act
            new ComPreritServicesRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Has.Some.Matches((IHandler h) => h.ComponentModel.Implementation.Name.EndsWith("Service")));
        }

        [Test]
        public void Should_Register_IEmailSenderService_With_SmtpHost_Parameter()
        {
            // arrange
            var container = new WindsorContainer();
            IHandler handler = null;

            container.Kernel.HandlerRegistered += (IHandler h, ref bool stateChanged) =>
                                                      {
                                                          if (h.Service == typeof(IEmailSenderService))
                                                          {
                                                              handler = h;
                                                          }
                                                      };

            // act
            new ComPreritServicesRegistration().Register(container.Kernel);

            // assert
            Assert.That(handler, Is.Not.Null);
            Assert.That(handler.ComponentModel.Configuration.Children[0].Children[0].Value, Is.EqualTo(EmailInfo.SmtpHost));
        }

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

        [Test]
        public void Should_Register_Transient_Services()
        {
            // arrange
            var container = new WindsorContainer();
            var handlers = new List<IHandler>();

            container.Kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) => handlers.Add(handler);

            // act
            new ComPreritServicesRegistration().Register(container.Kernel);

            // assert
            Assert.That(handlers, Has.All.Matches((IHandler h) => h.ComponentModel.LifestyleType == LifestyleType.Transient));
        }

        #endregion
    }
}