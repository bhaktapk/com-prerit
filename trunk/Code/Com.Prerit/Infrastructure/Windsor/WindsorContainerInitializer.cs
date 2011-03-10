using System;
using System.Reflection;

using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class WindsorContainerInitializer
    {
        #region Methods

        public void Init(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            container.Register(
                AllTypes.Of<IRegistration>().FromAssembly(Assembly.GetExecutingAssembly())
                    .WithService.FirstInterface()
            );

            container.Register(container.ResolveAll<IRegistration>());
        }

        #endregion
    }
}