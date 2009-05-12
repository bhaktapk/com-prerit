using CommonServiceLocator.WindsorAdapter;

using Microsoft.Practices.ServiceLocation;

namespace Com.Prerit.Web
{
    public static class StartupTaskRunner
    {
        #region Constructors

        static StartupTaskRunner()
        {
            ConfigureCommonServiceLocator();
        }

        #endregion

        #region Methods

        private static void ConfigureCommonServiceLocator()
        {
            IServiceLocator serviceLocator = new WindsorServiceLocator(new WindsorContainerInitializer().Init());

            ServiceLocator.SetLocatorProvider(() => serviceLocator);
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