using System;
using System.Diagnostics;

namespace Com.Prerit.Services
{
    public class AsyncCacheItemLoaderService : IAsyncCacheItemLoaderService
    {
        #region Methods

        private void Callback(IAsyncResult asyncResult)
        {
            Action loadAndSetCacheItem = (Action) asyncResult.AsyncState;

            try
            {
                loadAndSetCacheItem.EndInvoke(asyncResult);
            }
            catch (Exception e)
            {
                Trace.TraceError("Async cache item loader error:{0}{0}{1}", Environment.NewLine, e);
            }
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

            Action loadAndSetCacheItem = () => cacheItemSetter(loaderService.Load());

            return loadAndSetCacheItem.BeginInvoke(Callback, loadAndSetCacheItem);
        }

        #endregion
    }
}