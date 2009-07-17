using System.Net;
using System.Web;

namespace Com.Prerit.Web.Infrastructure.HttpModules
{
    public class CustomErrorsModule : IHttpModule
    {
        #region Constants

        public const string ForbiddenPath = "~/error/forbidden.htm";

        public const string GenericErrorPath = "~/error/generic-error.htm";

        public const string NotFoundPath = "~/error/not-found.htm";

        #endregion

        #region Methods

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.Error += (sender, e) => OnError(new HttpContextWrapper(((HttpApplication) sender).Context));
        }

        public void OnError(HttpContextBase context)
        {
            string errorPath;

            var exception = context.Server.GetLastError() as HttpException;

            HttpStatusCode httpCode = exception != null ? (HttpStatusCode) exception.GetHttpCode() : HttpStatusCode.InternalServerError;

            switch (httpCode)
            {
                case HttpStatusCode.Forbidden:
                    errorPath = ForbiddenPath;
                    break;
                case HttpStatusCode.NotFound:
                    errorPath = NotFoundPath;
                    break;
                default:
                    errorPath = GenericErrorPath;
                    break;
            }

            context.ClearError();
            context.Response.StatusCode = (int) httpCode;
            context.Server.Transfer(errorPath);
        }

        #endregion
    }
}