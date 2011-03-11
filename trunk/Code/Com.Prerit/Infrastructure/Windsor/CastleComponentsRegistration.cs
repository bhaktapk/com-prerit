using System.Reflection;

using Castle.Components.Validator;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class CastleComponentsRegistration : RegistrationBase
    {
        #region Fields

        private readonly Assembly _assembly = typeof(IValidatorRunner).Assembly;

        #endregion

        #region Methods

        public override void Register(IKernel kernel)
        {
            RegisterServices(kernel);
        }

        private void RegisterServices(IKernel kernel)
        {
            kernel.Register(AllTypes.Pick().FromAssembly(_assembly).WithService.FirstInterface());
        }

        #endregion
    }
}