namespace Com.Prerit.Web
{
    public static class StartupTaskRunner
    {
        #region Methods

        public static void Run()
        {
            var tasks = new IStartupTask[]
                            {
                                new RegisterRoutesStartupTask()
                            };

            foreach (IStartupTask task in tasks)
            {
                task.Execute();
            }
        }

        #endregion
    }
}