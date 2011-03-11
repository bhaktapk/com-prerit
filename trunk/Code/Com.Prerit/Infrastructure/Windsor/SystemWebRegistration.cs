using System.Web;
using System.Web.Caching;
using System.Web.Routing;

using Castle.Facilities.FactorySupport;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class SystemWebRegistration : RegistrationBase
    {
        #region Methods

        public override void Register(IKernel kernel)
        {
            AddFacility<FactorySupportFacility>(kernel);

            RegisterCache(kernel);
            RegisterHttpContextBase(kernel);
            RegisterHttpRequestBase(kernel);
            RegisterHttpResponseBase(kernel);
            RegisterHttpServerUtilityBase(kernel);
            RegisterHttpSessionStateBase(kernel);
            RegisterRouteCollection(kernel);
        }

        private void RegisterCache(IKernel kernel)
        {
            kernel.Register(
                Component.For<Cache>()
                    .UsingFactoryMethod(() => HttpRuntime.Cache)
            );
        }

        private void RegisterHttpContextBase(IKernel kernel)
        {
            kernel.Register(
                Component.For<HttpContextBase>()
                    .LifeStyle.PerWebRequest
                    .UsingFactoryMethod(() => new HttpContextWrapper(HttpContext.Current))
            );
        }

        private void RegisterHttpRequestBase(IKernel kernel)
        {
            kernel.Register(
                Component.For<HttpRequestBase>()
                    .LifeStyle.PerWebRequest
                    .UsingFactoryMethod(k => k.Resolve<HttpContextBase>().Request)
            );
        }

        private void RegisterHttpResponseBase(IKernel kernel)
        {
            kernel.Register(
                Component.For<HttpResponseBase>()
                    .LifeStyle.PerWebRequest
                    .UsingFactoryMethod(k => k.Resolve<HttpContextBase>().Response)
            );
        }

        private void RegisterHttpServerUtilityBase(IKernel kernel)
        {
            kernel.Register(
                Component.For<HttpServerUtilityBase>()
                    .LifeStyle.PerWebRequest
                    .UsingFactoryMethod(k => k.Resolve<HttpContextBase>().Server)
            );
        }

        private void RegisterHttpSessionStateBase(IKernel kernel)
        {
            kernel.Register(
                Component.For<HttpSessionStateBase>()
                    .LifeStyle.PerWebRequest
                    .UsingFactoryMethod(k => k.Resolve<HttpContextBase>().Session)
                );
        }

        private void RegisterRouteCollection(IKernel kernel)
        {
            kernel.Register(
                Component.For<RouteCollection>()
                    .UsingFactoryMethod(() => RouteTable.Routes)
            );
        }

        #endregion
    }
}