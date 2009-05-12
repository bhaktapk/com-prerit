using Castle.Components.Validator;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

using Com.Prerit.Services;

namespace Com.Prerit.Web
{
    public class WindsorContainerInitializer
    {
        #region Methods

        public IWindsorContainer Init()
        {
            var windsorContainer = new WindsorContainer();

            windsorContainer
                .Register(
                    Component.For<IEmailSenderService>()
                        .ImplementedBy<EmailSenderService>()
                            .Parameters(Parameter.ForKey("smtpHost").Eq(EmailInfo.SmtpHost)))
                .Register(
                    Component.For<IValidatorRunner>()
                        .ImplementedBy<ValidatorRunner>())
                .Register(
                    Component.For<IValidatorRegistry>()
                        .ImplementedBy<CachedValidationRegistry>())
                .Register(
                    Component.For<IStartupTask>()
                        .ImplementedBy<RegisterRoutesStartupTask>(),
                    Component.For<IStartupTask>()
                        .ImplementedBy<RegisterDefaultModelBinderStartupTask>());

            return windsorContainer;
        }

        #endregion
    }
}