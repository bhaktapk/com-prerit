using System.Web;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class SystemWebRegistration : IRegistration
    {
        public void Register(IKernel kernel)
        {
            kernel
                .Register(Component.For<HttpContextBase>()
                    .LifeStyle.PerWebRequest
                    .UsingFactoryMethod(() => new HttpContextWrapper(HttpContext.Current)))
                .Register(Component.For<HttpRequestBase>()
                    .LifeStyle.PerWebRequest
                    .UsingFactoryMethod(k => k.Resolve<HttpContextBase>().Request))
                .Register(Component.For<HttpResponseBase>()
                    .LifeStyle.PerWebRequest
                    .UsingFactoryMethod(k => k.Resolve<HttpContextBase>().Response))
                .Register(Component.For<HttpSessionStateBase>()
                    .LifeStyle.PerWebRequest
                    .UsingFactoryMethod(k => k.Resolve<HttpContextBase>().Session));
        }
    }
}