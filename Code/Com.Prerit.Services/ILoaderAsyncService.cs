namespace Com.Prerit.Services
{
    public interface ILoaderAsyncService<T>
    {
        #region Properties

        T LoadedObject { get; }

        LoaderAsyncServiceStatus Status { get; }

        #endregion

        #region Methods

        void LoadAsync();

        #endregion
    }
}