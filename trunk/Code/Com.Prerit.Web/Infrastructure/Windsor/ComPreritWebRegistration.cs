using System.Web.Mvc;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

using Com.Prerit.Web.Infrastructure.MapCreators;
using Com.Prerit.Web.Infrastructure.StartupTasks;

namespace Com.Prerit.Web.Infrastructure.Windsor
{
    public class ComPreritWebRegistration : IRegistration
    {
        public void Register(IKernel kernel)
        {
            const string assemblyName = "Com.Prerit.Web";

            kernel
                .Register(
                    AllTypes.Of<IController>().FromAssemblyNamed(assemblyName)
                        .Configure(c => c.LifeStyle.Transient))
                .Register(
                    AllTypes.Of<IMapCreator>().FromAssemblyNamed(assemblyName))
                .Register(
                    AllTypes.Of<IModelBinder>().FromAssemblyNamed(assemblyName))
                .Register(
                    AllTypes.Of<IStartupTask>().FromAssemblyNamed(assemblyName));
        }
    }
}