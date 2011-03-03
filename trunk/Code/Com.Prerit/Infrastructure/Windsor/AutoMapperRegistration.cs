﻿using AutoMapper;
using AutoMapper.Mappers;

using Castle.Facilities.FactorySupport;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class AutoMapperRegistration : RegistrationBase, IRegistration
    {
        #region Methods

        public void Register(IKernel kernel)
        {
            AddFacility<FactorySupportFacility>(kernel);

            RegisterConfiguration(kernel);
            RegisterIMappingEngine(kernel);
        }

        private void RegisterConfiguration(IKernel kernel)
        {
            kernel.Register(
                Component.For<IConfiguration, IConfigurationProvider, IFormatterExpression, IProfileConfiguration, IProfileExpression>()
                    .LifeStyle.Transient
                    .UsingFactoryMethod(k => new Configuration(MapperRegistry.AllMappers()))
            );
        }

        private void RegisterIMappingEngine(IKernel kernel)
        {
            kernel.Register(
                Component.For<IMappingEngine>()
                    .LifeStyle.Transient
                    .UsingFactoryMethod(k => new MappingEngine(kernel.Resolve<IConfigurationProvider>()))
            );
        }

        #endregion
    }
}