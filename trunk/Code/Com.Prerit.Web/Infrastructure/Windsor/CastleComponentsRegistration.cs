using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Com.Prerit.Web.Infrastructure.Windsor
{
    public class CastleComponentsRegistration : IRegistration
    {
        public void Register(IKernel kernel)
        {
            const string assemblyName = "Castle.Components.Validator";

            kernel
                .Register(
                    AllTypes.Pick().FromAssemblyNamed(assemblyName)
                        .WithService.FirstInterface());
        }
    }
}