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

        protected void AddSubResolver<T>(IKernel kernel, T resolver) where T : class, ISubDependencyResolver
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }

            if (resolver == null)
            {
                throw new ArgumentNullException("resolver");
            }

            // NOTE: there currently isn't a way to check to see if a resolver has already been added in order to prevent duplicate resolvers

            kernel.Resolver.AddSubResolver(resolver);
        }

        #endregion
    }
}