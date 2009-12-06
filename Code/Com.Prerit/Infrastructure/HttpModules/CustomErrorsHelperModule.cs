using System.Net;
using System.Web;

namespace Com.Prerit.Infrastructure.HttpModules
{
    public class CustomErrorsHelperModule : IHttpModule
    {
        #region Methods

        public void Dispose()
        {
        }

        private HttpStatusCode GetHttpStatusCode(HttpContextBase context)
        {
            var exception = context.Server.GetLastError() as HttpException;

            return exception != null ? (HttpStatusCode) exception.GetHttpCode() : HttpStatusCode.InternalServerError;
        }

        public void Init(HttpApplication context)
        {
            context.Error += (sender, e) => OnError(new HttpContextWrapper(((HttpApplication) sender).Context));
        }

        public void OnError(HttpContextBase context)
        {
            context.Response.AddHeader("Content-Type", string.Format("{0}; charset={1}", context.Response.ContentType, context.Response.ContentEncoding.WebName));
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.StatusCode = (int) GetHttpStatusCode(context);
        }

        #endregion
    }
}