using System.Web.Hosting;
using System.Web.Mvc;

using Castle.Facilities.FactorySupport;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;

using Com.Prerit.Infrastructure.HttpApplications;
using Com.Prerit.Infrastructure.ModelBinders;
using Com.Prerit.Infrastructure.Routing;
using Com.Prerit.Infrastructure.StartupTasks;
using Com.Prerit.MapCreators;
using Com.Prerit.Services;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class ComPreritRegistration : RegistrationBase, IRegistration
    {
        #region Fields

        private readonly string _assemblyName = typeof(MvcApplication).Assembly.FullName;

        #endregion

        #region Methods

        public void Register(IKernel kernel)
        {
            AddFacility<FactorySupportFacility>(kernel);

            AddSubResolver(kernel, new ArrayResolver(kernel));

            RegisterServices(kernel);
            RegisterControllers(kernel);
            RegisterMapCreators(kernel);
            RegisterModelBinder(kernel);
            RegisterRouteValueOptimizers(kernel);
            RegisterStartupTasks(kernel);
        }

        private void RegisterControllers(IKernel kernel)
        {
            kernel.Register(
                AllTypes.Of<IController>().FromAssemblyNamed(_assemblyName)
                    .Configure(c => c.LifeStyle.Transient)
            );
        }

        private void RegisterMapCreators(IKernel kernel)
        {
            kernel.Register(
                AllTypes.Of<IMapCreator>().FromAssemblyNamed(_assemblyName)
                    .WithService.FirstInterface()
            );
        }

        private void RegisterModelBinder(IKernel kernel)
        {
            kernel.Register(
                Component.For<IModelBinder>()
                    .ImplementedBy<SimpleValidatingModelBinder>()
            );
        }

        private void RegisterRouteValueOptimizers(IKernel kernel)
        {
            kernel.Register(
                Component.For<IRouteValueOptimizer>()
                    .ImplementedBy<RouteConstraintOptimizer>()
                        .Named(typeof(ListConstraint).FullName),
                Component.For<IRouteValueOptimizer>()
                    .ImplementedBy<StringOptimizer>()
                        .Named(typeof(string).FullName)
            );
        }

        private void RegisterServices(IKernel kernel)
        {
            kernel.Register(
                AllTypes.Pick().FromAssemblyNamed(_assemblyName)
                    .If(t => t.Name.EndsWith("Service"))
                    .Configure(c => c.LifeStyle.Transient)
                    .WithService.FirstInterface()
                    .ConfigureFor<IAlbumService>(c => c.Parameters(Parameter.ForKey("albumRootDirPath").Eq(HostingEnvironment.MapPath("~/App_Data/Albums/"))))
                    .ConfigureFor<IProfileService>(c => c.Parameters(Parameter.ForKey("profilesDirectoryPath").Eq(HostingEnvironment.MapPath("~/App_Data/Profiles/"))))
                    .ConfigureFor<IRoleService>(c => c.Parameters(Parameter.ForKey("rolesDirectoryPath").Eq(HostingEnvironment.MapPath("~/App_Data/Roles/"))))
            );
        }

        private void RegisterStartupTasks(IKernel kernel)
        {
            kernel.Register(
                AllTypes.Of<IStartupTask>().FromAssemblyNamed(_assemblyName)
                    .WithService.FirstInterface()
            );
        }

        #endregion
    }
}