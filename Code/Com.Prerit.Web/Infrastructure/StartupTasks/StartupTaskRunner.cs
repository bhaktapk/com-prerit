using System;
using System.Web.Mvc;

using Castle.Windsor;

using Com.Prerit.Web.Infrastructure.Windsor;

using CommonServiceLocator.WindsorAdapter;

using Microsoft.Practices.ServiceLocation;

namespace Com.Prerit.Web.Infrastructure.StartupTasks
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
            Run(WindsorContainerInitializer.Init());
        }

        public static void Run(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            ConfigureCommonServiceLocator(new WindsorServiceLocator(container));

            ConfigureControllerFactory(new WindsorControllerFactory(container));

            foreach (IStartupTask task in ServiceLocator.Current.GetAllInstances<IStartupTask>())
            {
                task.Execute();
            }
        }

        #endregion
    }
}