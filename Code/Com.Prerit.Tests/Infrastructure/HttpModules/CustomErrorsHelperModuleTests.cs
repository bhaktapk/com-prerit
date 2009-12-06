using System.Net;
using System.Text;
using System.Web;

using Com.Prerit.Web.Infrastructure.HttpModules;

using Moq;

using NUnit.Framework;

namespace Com.Prerit.Tests.Infrastructure.HttpModules
{
    [TestFixture]
    public class CustomErrorsHelperModuleTests
    {
        #region Constants

        private const HttpStatusCode _statusCode = HttpStatusCode.NotFound;

        #endregion

        #region Fields

        private Mock<Encoding> _encoding;

        private Mock<HttpCachePolicyBase> _httpCachePolicyBase;

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
            _httpCachePolicyBase = new Mock<HttpCachePolicyBase>();
            _encoding = new Mock<Encoding>();

            _httpContext.SetupGet(c => c.Response).Returns(_httpResponse.Object);
            _httpContext.SetupGet(c => c.Server).Returns(_httpServerUtility.Object);

            _httpResponse.SetupGet(c => c.Cache).Returns(_httpCachePolicyBase.Object);
            _httpResponse.SetupGet(response => response.ContentType).Returns("text/html");
            _httpResponse.SetupGet(response => response.ContentEncoding).Returns(_encoding.Object);

            _encoding.Setup(e => e.WebName).Returns("utf-8");

            _httpServerUtility.Setup(server => server.GetLastError()).Returns(new HttpException((int) _statusCode, ""));
        }

        #endregion

        #region Tests

        [Test]
        public void Should_Add_Content_Type_Header()
        {
            // arrange
            var module = new CustomErrorsHelperModule();

            // act
            module.OnError(_httpContext.Object);

            // assert
            _httpResponse.Verify(response => response.AddHeader("Content-Type", "text/html; charset=utf-8"));
        }

        [Test]
        public void Should_Set_Cacheability()
        {
            // arrange
            var module = new CustomErrorsHelperModule();

            // act
            module.OnError(_httpContext.Object);

            // assert
            _httpCachePolicyBase.Verify(cache => cache.SetCacheability(HttpCacheability.NoCache));
        }

        [Test]
        public void Should_Set_Http_Status_Code()
        {
            // arrange
            var module = new CustomErrorsHelperModule();

            // act
            module.OnError(_httpContext.Object);

            // assert
            _httpResponse.VerifySet(response => response.StatusCode = (int) _statusCode);
        }

        #endregion
    }
}