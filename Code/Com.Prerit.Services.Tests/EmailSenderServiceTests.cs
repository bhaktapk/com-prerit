using Castle.Components.Validator;

using Com.Prerit.Domain;

using NUnit.Framework;

namespace Com.Prerit.Services.Tests
{
    [TestFixture]
    public class EmailSenderServiceTests
    {
        #region Fields

        private EmailSenderService _emailSenderService;

        #endregion

        #region Setup/Teardown

        [TestFixtureSetUp]
        public void SetUp()
        {
            _emailSenderService = new EmailSenderService("localhost", new ValidatorRunner(new CachedValidationRegistry()));
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