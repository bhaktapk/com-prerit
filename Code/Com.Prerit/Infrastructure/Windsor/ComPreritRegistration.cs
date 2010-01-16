using System.Web.Mvc;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

using Com.Prerit.Infrastructure.StartupTasks;
using Com.Prerit.MapCreators;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class ComPreritRegistration : IRegistration
    {
        #region Methods

        public void Register(IKernel kernel)
        {
            const string assemblyName = "Com.Prerit";

            kernel
                .Register(
                    AllTypes.Pick().FromAssemblyNamed(assemblyName)
                        .If(t => t.Name.EndsWith("Service"))
                        .Configure(c => c.LifeStyle.Transient)
                        .WithService.FirstInterface())
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

        #endregion
    }
}