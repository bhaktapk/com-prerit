namespace Com.Prerit.Services
{
    public interface ILoaderService<T>
    {
        #region Methods

        T Load();

        #endregion
    }
}