using AutoMapper;

using Com.Prerit.MapCreators;

using Microsoft.Practices.ServiceLocation;

namespace Com.Prerit.Infrastructure.StartupTasks
{
    public class AutoMapperConfigurationStartupTask : IStartupTask
    {
        #region Methods

        public void Execute()
        {
            foreach (IMapCreator mapCreator in ServiceLocator.Current.GetAllInstances<IMapCreator>())
            {
                mapCreator.CreateMap();
            }

            Mapper.AssertConfigurationIsValid();
        }

        public void Reset()
        {
            Mapper.Reset();
        }

        #endregion
    }
}