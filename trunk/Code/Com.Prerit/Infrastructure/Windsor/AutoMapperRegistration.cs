using AutoMapper;
using AutoMapper.Mappers;

using Castle.Facilities.FactorySupport;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class AutoMapperRegistration : RegistrationBase
    {
        #region Methods

        public override void Register(IKernel kernel) 
        {
            AddFacility<FactorySupportFacility>(kernel);

            RegisterConfiguration(kernel);
            RegisterIMappingEngine(kernel);
        }

        private void RegisterConfiguration(IKernel kernel)
        {
            kernel.Register(
                Component.For<IConfigurationProvider, IProfileExpression>()
                    .UsingFactoryMethod(k => new Configuration(MapperRegistry.AllMappers()))
            );
        }

        private void RegisterIMappingEngine(IKernel kernel)
        {
            kernel.Register(
                Component.For<IMappingEngine>()
                    .UsingFactoryMethod(k => new MappingEngine(kernel.Resolve<IConfigurationProvider>()))
            );
        }

        #endregion
    }
}