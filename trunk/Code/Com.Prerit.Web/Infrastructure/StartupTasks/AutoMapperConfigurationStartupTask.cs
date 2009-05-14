using AutoMapper;

using Com.Prerit.Web.Infrastructure.MapCreators;

using Microsoft.Practices.ServiceLocation;

namespace Com.Prerit.Web.Infrastructure.StartupTasks
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

        #endregion
    }
}