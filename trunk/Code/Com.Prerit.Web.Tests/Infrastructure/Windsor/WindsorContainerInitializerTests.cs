using Castle.MicroKernel.Registration;
using Castle.Windsor;

using Com.Prerit.Web.Infrastructure.Windsor;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.Windsor
{
    [TestFixture]
    public class WindsorContainerInitializerTests
    {
        #region Tests

        [Test]
        public void Should_Initialize_Windsor_Container()
        {
            IWindsorContainer container = WindsorContainerInitializer.Init();

            Assert.That(container.ResolveAll<IRegistration>(), Is.Not.Empty);
        }

        #endregion
    }
}