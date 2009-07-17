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

            _httpContext.SetupGet(context => context.Request).Returns(_httpRequest.Object);
            _httpContext.SetupGet(context => context.Response).Returns(_httpResponse.Object);
            _httpContext.SetupGet(context => context.Server).Returns(_httpServerUtility.Object);
        }

        #endregion

        #region Tests

        [Test]
        public void Should_Clear_Error_From_Request()
        {
            var module = new CustomErrorsModule();

            module.OnError(_httpContext.Object);
            module.Dispose();

            _httpContext.Verify(context => context.ClearError());
        }

        [Test]
        public void Should_Show_Forbidden_Page()
        {
            var module = new CustomErrorsModule();

            _httpServerUtility.Setup(server => server.GetLastError()).Returns(new HttpException((int) HttpStatusCode.Forbidden, ""));

            module.OnError(_httpContext.Object);
            module.Dispose();

            _httpResponse.VerifySet(response => response.StatusCode = (int) HttpStatusCode.Forbidden);

            _httpServerUtility.Verify(server => server.Transfer(CustomErrorsModule.ForbiddenPath));
        }

        [Test]
        public void Should_Show_Generic_Error_Page()
        {
            var module = new CustomErrorsModule();

            _httpServerUtility.Setup(server => server.GetLastError()).Returns(new HttpException((int) HttpStatusCode.InternalServerError, ""));

            module.OnError(_httpContext.Object);
            module.Dispose();

            _httpResponse.VerifySet(response => response.StatusCode = (int) HttpStatusCode.InternalServerError);

            _httpServerUtility.Verify(server => server.Transfer(CustomErrorsModule.GenericErrorPath));
        }

        [Test]
        public void Should_Show_Not_Found_Page()
        {
            var module = new CustomErrorsModule();

            _httpServerUtility.Setup(server => server.GetLastError()).Returns(new HttpException((int) HttpStatusCode.NotFound, ""));

            module.OnError(_httpContext.Object);
            module.Dispose();

            _httpResponse.VerifySet(response => response.StatusCode = (int) HttpStatusCode.NotFound);

            _httpServerUtility.Verify(server => server.Transfer(CustomErrorsModule.NotFoundPath));
        }

        #endregion
    }
}