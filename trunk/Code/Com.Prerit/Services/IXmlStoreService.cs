namespace Com.Prerit.Services
{
    public interface IXmlStoreService
    {
        #region Methods

        T Load<T>(string filePath);

        void Save<T>(string filePath, T obj);

        #endregion
    }
}