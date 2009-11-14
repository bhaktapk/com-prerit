using System.Web;

namespace Com.Prerit.Web.Infrastructure.HttpModules
{
    public class ServiceUnavailableModule : IHttpModule
    {
        #region Methods

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) => OnBeginRequest();
            context.Error += (sender, e) => OnError(new HttpContextWrapper(((HttpApplication) sender).Context));
        }

        public void OnBeginRequest()
        {
            throw new HttpException(503, "Service Unavailable");
        }

        public void OnError(HttpContextBase context)
        {
            context.Response.AddHeader("Retry-After", "86400");
        }

        #endregion
    }
}