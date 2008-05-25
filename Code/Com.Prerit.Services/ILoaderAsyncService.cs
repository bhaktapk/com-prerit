namespace Com.Prerit.Services
{
    public interface ILoaderAsyncService<T>
    {
        #region Methods

        T GetLoadedObject();

        bool IsLoading();

        void LoadAsync();

        #endregion
    }
}