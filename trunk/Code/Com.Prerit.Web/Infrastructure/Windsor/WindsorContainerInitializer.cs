using System.Reflection;

using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Com.Prerit.Web.Infrastructure.Windsor
{
    public static class WindsorContainerInitializer
    {
        #region Methods

        public static IWindsorContainer Init()
        {
            var container = new WindsorContainer();

            container
                .Register(
                    AllTypes.Of<IRegistration>().FromAssembly(Assembly.GetExecutingAssembly()));

            foreach (IRegistration registration in container.ResolveAll<IRegistration>())
            {
                container.Register(registration);
            }

            return container;
        }

        #endregion
    }
}