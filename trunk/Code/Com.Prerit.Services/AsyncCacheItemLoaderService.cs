using System;
using System.Diagnostics;

namespace Com.Prerit.Services
{
    public class AsyncCacheItemLoaderService : IAsyncCacheItemLoaderService
    {
        #region Methods

        private void LoadAndSetCacheItem<T>(ILoaderService<T> loaderService, Action<T> cacheItemSetter)
        {
            Debug.Assert(loaderService != null);
            Debug.Assert(cacheItemSetter != null);

            try
            {
                cacheItemSetter(loaderService.Load());
            }
            catch (Exception e)
            {
                Trace.TraceError("Async cache item loader error:{0}{0}{1}", Environment.NewLine, e);
            }
        }

        public bool IsLoading()
        {
            throw new NotImplementedException();
        }

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

            Action<ILoaderService<T>, Action<T>> loadoadAndSetCacheItem = LoadAndSetCacheItem;

            return loadoadAndSetCacheItem.BeginInvoke(loaderService, cacheItemSetter, asyncResult => loadoadAndSetCacheItem.EndInvoke(asyncResult), null);
        }

        #endregion
    }
}