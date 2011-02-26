using System;
using System.Linq;

using Castle.MicroKernel;

namespace Com.Prerit.Infrastructure.Windsor
{
    public abstract class RegistrationBase
    {
        #region Methods

        protected void AddFacility<T>(IKernel kernel) where T : IFacility, new()
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }

            IFacility[] facilities = kernel.GetFacilities();

            if (facilities != null && facilities.Any(f => f.GetType() == typeof(T)))
            {
                return;
            }

            kernel.AddFacility<T>();
        }

        #endregion
    }
}