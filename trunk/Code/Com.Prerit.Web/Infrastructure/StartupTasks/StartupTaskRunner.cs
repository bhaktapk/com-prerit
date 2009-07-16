using System.Web.Mvc;

using Castle.Windsor;

using Com.Prerit.Web.Infrastructure.Windsor;

using CommonServiceLocator.WindsorAdapter;

using Microsoft.Practices.ServiceLocation;

namespace Com.Prerit.Web.Infrastructure.StartupTasks
{
    public static class StartupTaskRunner
    {
        #region Constructors

        static StartupTaskRunner()
        {
            IWindsorContainer container = WindsorContainerInitializer.Init();

            ConfigureCommonServiceLocator(container);
            ConfigureControllerFactory(container);
        }

        #endregion

        #region Methods

        private static void ConfigureCommonServiceLocator(IWindsorContainer container)
        {
            IServiceLocator serviceLocator = new WindsorServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        private static void ConfigureControllerFactory(IWindsorContainer container)
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }

        public static void Reset()
        {
            foreach (IStartupTask task in ServiceLocator.Current.GetAllInstances<IStartupTask>())
            {
                task.Reset();
            }
        }

        public static void Run()
        {
            foreach (IStartupTask task in ServiceLocator.Current.GetAllInstances<IStartupTask>())
            {
                task.Execute();
            }
        }

        #endregion
    }
}