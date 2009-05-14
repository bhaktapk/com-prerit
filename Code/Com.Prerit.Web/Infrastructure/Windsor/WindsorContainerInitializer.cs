using System;
using System.Web.Mvc;

using Castle.MicroKernel.Registration;
using Castle.Windsor;

using Com.Prerit.Services;
using Com.Prerit.Web.Infrastructure.StartupTasks;

namespace Com.Prerit.Web.Infrastructure.Windsor
{
    public static class WindsorContainerInitializer
    {
        #region Methods

        public static IWindsorContainer Init()
        {
            var container = new WindsorContainer();

            InitFromCastleComponentsValidator(container);

            InitFromComPreritServices(container);

            InitFromComPreritWeb(container);

            return container;
        }

        public static void InitFromCastleComponentsValidator(WindsorContainer container)
        {
            const string assemblyName = "Castle.Components.Validator";

            container
                .Register(
                AllTypes.Pick().FromAssemblyNamed(assemblyName)
                    .WithService.FirstInterface());
        }

        public static void InitFromComPreritServices(WindsorContainer container)
        {
            const string assemblyName = "Com.Prerit.Services";

            container
                .Register(
                AllTypes.Pick().FromAssemblyNamed(assemblyName)
                    .If(t => t.Name.EndsWith("Service"))
                    .Configure(c => c.LifeStyle.Transient)
                    .ConfigureFor<IEmailSenderService>(c => c.Parameters(Parameter.ForKey("smtpHost").Eq(EmailInfo.SmtpHost)))
                    .WithService.FirstInterface());
        }

        public static void InitFromComPreritWeb(WindsorContainer container)
        {
            const string assemblyName = "Com.Prerit.Web";

            container
                .Register(
                AllTypes.Of<IController>().FromAssemblyNamed(assemblyName)
                    .If(t => t.IsPublic && !t.IsAbstract && typeof(IController).IsAssignableFrom(t) && t.Name.EndsWith("Controller", StringComparison.InvariantCultureIgnoreCase))
                    .Configure(c => c.LifeStyle.Transient))
                .Register(
                AllTypes.Of<IStartupTask>().FromAssemblyNamed(assemblyName));
        }

        #endregion
    }
}