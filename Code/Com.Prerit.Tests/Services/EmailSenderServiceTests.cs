using Castle.Components.Validator;

using Com.Prerit.Domain;
using Com.Prerit.Services;

using Moq;

using NUnit.Framework;

namespace Com.Prerit.Tests.Services
{
    [TestFixture]
    public class EmailSenderServiceTests
    {
        #region Tests

        [Test]
        public void Should_Fail_To_Send_Email_Because_Email_Is_Invalid()
        {
            // arrange
            const string errorMessage = "message";

            var email = new Email();
            var validatorRunner = new Mock<IValidatorRunner>();
            var errorSummary = new ErrorSummary();

            validatorRunner.Setup(v => v.IsValid(It.IsAny<Email>())).Returns(false);
            validatorRunner.Setup(v => v.GetErrorSummary(It.IsAny<Email>())).Returns(errorSummary);

            errorSummary.RegisterErrorMessage(errorMessage, errorMessage);

            // act
            var service = new EmailSenderService(validatorRunner.Object);

            TestDelegate act = () => service.Send(email);

            // assert
            Assert.That(act, Throws.InstanceOf<ValidationException>().With.Property("ValidationErrorMessages").EqualTo(new[] { errorMessage }));
        }

        #endregion
    }
}