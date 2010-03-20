using AutoMapper;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Com.Prerit.Infrastructure.Windsor
{
    public class AutoMapperRegistration : IRegistration
    {
        #region Methods

        public void Register(IKernel kernel)
        {
            const string assemblyName = "AutoMapper";

            kernel
                .Register(Component.For<IMappingEngine>()
                    .UsingFactoryMethod(() => Mapper.Engine));
        }

        #endregion
    }
}