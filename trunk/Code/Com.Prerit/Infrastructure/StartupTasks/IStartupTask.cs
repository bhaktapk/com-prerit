namespace Com.Prerit.Infrastructure.StartupTasks
{
    public interface IStartupTask
    {
        #region Methods

        void Execute();

        void Reset();

        #endregion
    }
}