using System.Net;
using System.Web;

namespace Com.Prerit.Web.Infrastructure.HttpModules
{
    public class CustomErrorsHelperModule : IHttpModule
    {
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
            var exception = context.Server.GetLastError() as HttpException;

            HttpStatusCode httpCode = exception != null ? (HttpStatusCode) exception.GetHttpCode() : HttpStatusCode.InternalServerError;

            context.Response.StatusCode = (int) httpCode;
        }

        #endregion
    }
}