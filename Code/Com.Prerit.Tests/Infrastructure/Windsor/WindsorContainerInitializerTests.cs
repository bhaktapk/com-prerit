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
        public void Should_Initialize_Windsor_Container()
        {
            //arrange
            IWindsorContainer container;

            //act
            container = WindsorContainerInitializer.Init();

            //assert
            Assert.That(container.ResolveAll<IRegistration>(), Is.Not.Null.And.Not.Empty);
        }

        #endregion
    }
}