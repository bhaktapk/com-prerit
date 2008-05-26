namespace Com.Prerit.Services
{
    public interface ILoaderAsyncService<T>
    {
        #region Methods

        T GetLoadedObject();

        bool IsFailedLoad();

        bool IsLoading();

        void LoadAsync();

        #endregion
    }
}