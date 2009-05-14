using System.Net;
using System.Web;

namespace Com.Prerit.Web.Infrastructure.HttpModules
{
    public class CustomErrorsModule : IHttpModule
    {
        #region Methods

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.Error += (sender, e) => OnError(new HttpContextWrapper(((HttpApplication) sender).Context));
        }

        public void OnError(HttpContextWrapper context)
        {
            string errorPath;

            var exception = context.Server.GetLastError() as HttpException;

            HttpStatusCode httpCode = exception != null ? (HttpStatusCode) exception.GetHttpCode() : HttpStatusCode.InternalServerError;

            switch (httpCode)
            {
                case HttpStatusCode.Forbidden:
                    errorPath = "~/error/forbidden.htm";
                    break;
                case HttpStatusCode.NotFound:
                    errorPath = "~/error/not-found.htm";
                    break;
                default:
                    errorPath = "~/error/generic-error.htm";
                    break;
            }

            context.ClearError();
            context.Response.StatusCode = (int) httpCode;
            context.Server.Transfer(errorPath);
        }

        #endregion
    }
}