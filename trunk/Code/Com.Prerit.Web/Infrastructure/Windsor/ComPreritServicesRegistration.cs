using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

using Com.Prerit.Services;

namespace Com.Prerit.Web.Infrastructure.Windsor
{
    public class ComPreritServicesRegistration : IRegistration
    {
        public void Register(IKernel kernel)
        {
            const string assemblyName = "Com.Prerit.Services";

            kernel
                .Register(
                    AllTypes.Pick().FromAssemblyNamed(assemblyName)
                        .If(t => t.Name.EndsWith("Service"))
                        .Configure(c => c.LifeStyle.Transient)
                        .ConfigureFor<IEmailSenderService>(c => c.Parameters(Parameter.ForKey("smtpHost").Eq(EmailInfo.SmtpHost)))
                        .WithService.FirstInterface());
        }
    }
}