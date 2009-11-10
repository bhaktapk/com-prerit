using System.Net;
using System.Web;

using Com.Prerit.Web.Infrastructure.HttpModules;

using Moq;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.HttpModules
{
    [TestFixture]
    public class CustomErrorsHelperModuleTests
    {
        #region Fields

        private Mock<HttpContextBase> _httpContext;

        private Mock<HttpResponseBase> _httpResponse;

        private Mock<HttpServerUtilityBase> _httpServerUtility;

        #endregion

        #region Setup/Teardown

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _httpContext = new Mock<HttpContextBase>();
            _httpResponse = new Mock<HttpResponseBase>();
            _httpServerUtility = new Mock<HttpServerUtilityBase>();

            _httpContext.SetupGet(c => c.Response).Returns(_httpResponse.Object);
            _httpContext.SetupGet(c => c.Server).Returns(_httpServerUtility.Object);
        }

        #endregion

        #region Tests

        [Test]
        public void Should_Set_Http_Status_Code()
        {
            // arrange
            const HttpStatusCode statusCode = HttpStatusCode.NotFound;

            var module = new CustomErrorsHelperModule();

            _httpServerUtility.Setup(server => server.GetLastError()).Returns(new HttpException((int) statusCode, ""));

            // act
            module.OnError(_httpContext.Object);
            module.Dispose();

            // assert
            _httpResponse.VerifySet(response => response.StatusCode = (int) statusCode);
        }

        #endregion
    }
}