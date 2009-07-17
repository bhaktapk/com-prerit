using Castle.Components.Validator;
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
        public void Should_Register_Services()
        {
            using (var container = new WindsorContainer())
            {
                container.Register(new CastleComponentsRegistration());

                Assert.That(container.Resolve(typeof(IValidator)), Is.Not.Null);
            }
        }

        #endregion
    }
}