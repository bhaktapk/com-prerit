using Castle.Components.Validator;

using Com.Prerit.Domain;
using Com.Prerit.Services;

using NUnit.Framework;

namespace Com.Prerit.Tests.Services
{
    [TestFixture]
    public class EmailSenderServiceTests
    {
        #region Fields

        private EmailSenderService _emailSenderService;

        #endregion

        #region Setup/Teardown

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _emailSenderService = new EmailSenderService(new ValidatorRunner(new CachedValidationRegistry()));
        }

        #endregion

        #region Tests

        [Test]
        public void Should_Fail_To_Send_Email_Because_Email_Is_Invalid()
        {
            var email = new Email();

            Assert.That(() => _emailSenderService.Send(email), Throws.InstanceOf<ValidationException>());
        }

        #endregion
    }
}