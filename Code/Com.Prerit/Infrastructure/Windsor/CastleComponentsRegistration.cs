using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class CastleComponentsRegistration : IRegistration
    {
        #region Methods

        public void Register(IKernel kernel)
        {
            const string assemblyName = "Castle.Components.Validator";

            kernel
                .Register(
                    AllTypes.Pick().FromAssemblyNamed(assemblyName)
                        .WithService.FirstInterface());
        }

        #endregion
    }
}