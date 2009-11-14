using System.Web;

using Com.Prerit.Web.Infrastructure.HttpModules;

using Moq;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.HttpModules
{
    [TestFixture]
    public class ServiceUnavailableModuleTests
    {
        #region Fields

        private Mock<HttpContextBase> _httpContext;

        private Mock<HttpResponseBase> _httpResponse;

        #endregion

        #region Setup/Teardown

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _httpContext = new Mock<HttpContextBase>();
            _httpResponse = new Mock<HttpResponseBase>();

            _httpContext.SetupGet(c => c.Response).Returns(_httpResponse.Object);
        }

        #endregion

        #region Tests

        [Test]
        public void Should_Add_Retry_After_Header()
        {
            // arrange
            var module = new ServiceUnavailableModule();

            // act
            module.OnError(_httpContext.Object);

            // assert
            _httpResponse.Verify(response => response.AddHeader("Retry-After", "86400"));
        }

        [Test]
        public void Should_Throw_HttpException()
        {
            // arrange
            var module = new ServiceUnavailableModule();

            // act
            TestDelegate act = module.OnBeginRequest;

            // assert
            var exception = Assert.Throws<HttpException>(act);

            Assert.That(exception.GetHttpCode(), Is.EqualTo(503));
        }

        #endregion
    }
}