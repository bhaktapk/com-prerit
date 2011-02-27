using Castle.Components.Validator;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class CastleComponentsRegistration : RegistrationBase, IRegistration
    {
        #region Methods

        public void Register(IKernel kernel)
        {
            string assemblyName = typeof(IValidatorRunner).Assembly.FullName;

            kernel
                .Register(
                    AllTypes.Pick().FromAssemblyNamed(assemblyName)
                        .WithService.FirstInterface());
        }

        #endregion
    }
}