using System;

namespace Com.Prerit.Services
{
    public interface IAsyncCacheItemLoaderService
    {
        #region Methods

        IAsyncResult LoadAsync<T>(ILoaderService<T> loaderService, Action<T> cacheItemSetter);

        #endregion
    }
}