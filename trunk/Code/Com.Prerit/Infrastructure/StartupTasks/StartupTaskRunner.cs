using System;
using System.Linq;
using System.Web.Mvc;

using Castle.MicroKernel;
using Castle.Windsor;

using Com.Prerit.Infrastructure.Windsor;

using CommonServiceLocator.WindsorAdapter;

using Microsoft.Practices.ServiceLocation;

using MvcContrib.Castle;

namespace Com.Prerit.Infrastructure.StartupTasks
{
    public static class StartupTaskRunner
    {
        #region Methods

        private static void ConfigureCommonServiceLocator(IServiceLocator serviceLocator)
        {
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        private static void ConfigureControllerFactory(IControllerFactory controllerFactory)
        {
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public static void Reset()
        {
            foreach (IStartupTask task in ServiceLocator.Current.GetAllInstances<IStartupTask>())
            {
                task.Reset();
            }

            ConfigureControllerFactory(new DefaultControllerFactory());

            ConfigureCommonServiceLocator(null);
        }

        public static void Run()
        {
            var container = new WindsorContainer();

            new WindsorContainerInitializer().Init(container);

            Run(container);
        }

        public static void Run(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            Verify(container.Kernel);

            ConfigureCommonServiceLocator(new WindsorServiceLocator(container));

            ConfigureControllerFactory(new WindsorControllerFactory(container));

            foreach (IStartupTask task in ServiceLocator.Current.GetAllInstances<IStartupTask>())
            {
                task.Execute();
            }
        }

        private static void Verify(IKernel kernel)
        {
            string[] handlers = (from handler in kernel.GetAssignableHandlers(typeof(object))
                                 where handler.CurrentState == HandlerState.WaitingDependency
                                 select handler.ComponentModel.Name).ToArray();

            if (handlers.Length != 0)
            {
                throw new ComponentRegistrationException("The following types could not be resolved: " + string.Join(", ", handlers));
            }
        }

        #endregion
    }
}