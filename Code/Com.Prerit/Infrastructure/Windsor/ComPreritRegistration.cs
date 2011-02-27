using System.Web.Hosting;
using System.Web.Mvc;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

using Com.Prerit.Infrastructure.HttpApplications;
using Com.Prerit.Infrastructure.Routing;
using Com.Prerit.Infrastructure.StartupTasks;
using Com.Prerit.MapCreators;
using Com.Prerit.Services;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class ComPreritRegistration : RegistrationBase, IRegistration
    {
        #region Methods

        public void Register(IKernel kernel)
        {
            string assemblyName = typeof(MvcApplication).Assembly.FullName;

            kernel
                .Register(
                    AllTypes.Pick().FromAssemblyNamed(assemblyName)
                        .If(t => t.Name.EndsWith("Service"))
                        .Configure(c => c.LifeStyle.Transient)
                        .WithService.FirstInterface()
                        .ConfigureFor<IAlbumService>(c => c.Parameters(Parameter.ForKey("albumRootDirPath").Eq(HostingEnvironment.MapPath("~/App_Data/Albums/"))))
                        .ConfigureFor<IProfileService>(c => c.Parameters(Parameter.ForKey("profilesDirectoryPath").Eq(HostingEnvironment.MapPath("~/App_Data/Profiles/"))))
                        .ConfigureFor<IRoleService>(c => c.Parameters(Parameter.ForKey("rolesDirectoryPath").Eq(HostingEnvironment.MapPath("~/App_Data/Roles/")))))
                .Register(
                    AllTypes.Of<IController>().FromAssemblyNamed(assemblyName)
                        .Configure(c => c.LifeStyle.Transient))
                .Register(
                    AllTypes.Of<IMapCreator>().FromAssemblyNamed(assemblyName))
                .Register(
                    AllTypes.Of<IModelBinder>().FromAssemblyNamed(assemblyName))
                .Register(
                    AllTypes.Of<IRouteValueOptimizer>().FromAssemblyNamed(assemblyName)
                        .ConfigureFor<RouteConstraintOptimizer>(c => c.Named(typeof(ListConstraint).FullName))
                        .ConfigureFor<StringOptimizer>(c => c.Named(typeof(string).FullName)))
                .Register(
                    AllTypes.Of<IStartupTask>().FromAssemblyNamed(assemblyName));
        }

        #endregion
    }
}