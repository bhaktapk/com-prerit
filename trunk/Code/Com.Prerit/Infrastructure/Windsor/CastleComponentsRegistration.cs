using Castle.Components.Validator;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class CastleComponentsRegistration : RegistrationBase, IRegistration
    {
        #region Fields

        private readonly string _assemblyName = typeof(IValidatorRunner).Assembly.FullName;

        #endregion

        #region Methods

        public void Register(IKernel kernel)
        {
            RegisterServices(kernel);
        }

        private void RegisterServices(IKernel kernel)
        {
            kernel.Register(
                AllTypes.Pick().FromAssemblyNamed(_assemblyName)
                    .WithService.FirstInterface()
            );
        }

        #endregion
    }
}