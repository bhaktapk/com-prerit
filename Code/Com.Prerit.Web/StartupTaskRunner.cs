using System.Web.Mvc;

using Castle.Windsor;

using CommonServiceLocator.WindsorAdapter;

using Microsoft.Practices.ServiceLocation;

namespace Com.Prerit.Web
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