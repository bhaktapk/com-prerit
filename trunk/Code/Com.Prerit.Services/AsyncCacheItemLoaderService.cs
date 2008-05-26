using System;
using System.Diagnostics;

namespace Com.Prerit.Services
{
    public class AsyncCacheItemLoaderService : IAsyncCacheItemLoaderService
    {
        #region Methods

        public IAsyncResult LoadAsync<T>(ILoaderService<T> loaderService, Action<T> cacheItemSetter)
        {
            if (loaderService == null)
            {
                throw new ArgumentNullException("loaderService");
            }

            if (cacheItemSetter == null)
            {
                throw new ArgumentNullException("cacheItemSetter");
            }

            Action loadAndSetCacheItem = () => cacheItemSetter(loaderService.Load());

            return loadAndSetCacheItem.BeginInvoke(loadAndSetCacheItem.EndInvoke, null);
        }

        #endregion
    }
}