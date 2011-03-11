using Castle.Components.Validator;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class CastleComponentsRegistration : RegistrationBase
    {
        #region Methods

        public override void Register(IKernel kernel)
        {
            RegisterIValidatorRegistry(kernel);
            RegisterIValidatorRunner(kernel);
        }

        private void RegisterIValidatorRegistry(IKernel kernel)
        {
            kernel.Register(
                Component.For(typeof(IValidatorRegistry))
                    .ImplementedBy(typeof(CachedValidationRegistry))
            );
        }

        private void RegisterIValidatorRunner(IKernel kernel)
        {
            kernel.Register(
                Component.For(typeof(IValidatorRunner))
                    .ImplementedBy(typeof(ValidatorRunner))
            );
        }

        #endregion
    }
}