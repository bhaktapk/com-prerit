namespace Com.Prerit.Web.Infrastructure.StartupTasks
{
    public interface IStartupTask
    {
        #region Methods

        void Execute();

        void Reset();

        #endregion
    }
}