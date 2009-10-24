using System.Net;
using System.Web;

using Com.Prerit.Web.Infrastructure.HttpModules;

using Moq;

using NUnit.Framework;

namespace Com.Prerit.Web.Tests.Infrastructure.HttpModules
{
    [TestFixture]
    public class CustomErrorsModuleTests
    {
        #region Fields

        private Mock<HttpContextBase> _httpContext;

        private Mock<HttpRequestBase> _httpRequest;

        private Mock<HttpResponseBase> _httpResponse;

        private Mock<HttpServerUtilityBase> _httpServerUtility;

        #endregion

        #region Setup/Teardown

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _httpContext = new Mock<HttpContextBase>();
            _httpRequest = new Mock<HttpRequestBase>();
            _httpResponse = new Mock<HttpResponseBase>();
            _httpServerUtility = new Mock<HttpServerUtilityBase>();

            _httpContext.SetupGet(c => c.Request).Returns(_httpRequest.Object);
            _httpContext.SetupGet(c => c.Response).Returns(_httpResponse.Object);
            _httpContext.SetupGet(c => c.Server).Returns(_httpServerUtility.Object);
        }

        #endregion

        #region Tests

        [Test]
        public void Should_Clear_Error_From_Request()
        {
            // arrange
            var module = new CustomErrorsModule();

            // act
            module.OnError(_httpContext.Object);
            module.Dispose();

            // assert
            _httpContext.Verify(c => c.ClearError());
        }

        [Test]
        public void Should_Show_Forbidden_Page()
        {
            // arrange
            var module = new CustomErrorsModule();

            _httpServerUtility.Setup(server => server.GetLastError()).Returns(new HttpException((int) HttpStatusCode.Forbidden, ""));

            // act
            module.OnError(_httpContext.Object);
            module.Dispose();

            // assert
            _httpResponse.VerifySet(response => response.StatusCode = (int) HttpStatusCode.Forbidden);

            _httpServerUtility.Verify(server => server.Transfer(CustomErrorsModule.ForbiddenPath));
        }

        [Test]
        public void Should_Show_Generic_Error_Page()
        {
            // arrange
            var module = new CustomErrorsModule();

            _httpServerUtility.Setup(server => server.GetLastError()).Returns(new HttpException((int) HttpStatusCode.InternalServerError, ""));

            // act
            module.OnError(_httpContext.Object);
            module.Dispose();

            // assert
            _httpResponse.VerifySet(response => response.StatusCode = (int) HttpStatusCode.InternalServerError);

            _httpServerUtility.Verify(server => server.Transfer(CustomErrorsModule.GenericErrorPath));
        }

        [Test]
        public void Should_Show_Not_Found_Page()
        {
            // arrange
            var module = new CustomErrorsModule();

            _httpServerUtility.Setup(server => server.GetLastError()).Returns(new HttpException((int) HttpStatusCode.NotFound, ""));

            // act
            module.OnError(_httpContext.Object);
            module.Dispose();

            // assert
            _httpResponse.VerifySet(response => response.StatusCode = (int) HttpStatusCode.NotFound);

            _httpServerUtility.Verify(server => server.Transfer(CustomErrorsModule.NotFoundPath));
        }

        #endregion
    }
}