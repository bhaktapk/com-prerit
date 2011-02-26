using System;
using System.Collections.Generic;

using AutoMapper;

using Com.Prerit.MapCreators;

namespace Com.Prerit.Infrastructure.StartupTasks
{
    public class AutoMapperConfigurationStartupTask : IStartupTask
    {
        #region Fields

        private readonly IConfigurationProvider _configurationProvider;

        private readonly IProfileExpression _profileExpression;

        private readonly IEnumerable<IMapCreator> _mapCreators;

        #endregion

        #region Constructors

        public AutoMapperConfigurationStartupTask(IConfigurationProvider configurationProvider, IProfileExpression profileExpression, IEnumerable<IMapCreator> mapCreators)
        {
            if (configurationProvider == null)
            {
                throw new ArgumentNullException("configurationProvider");
            }

            if (profileExpression == null)
            {
                throw new ArgumentNullException("profileExpression");
            }

            if (mapCreators == null)
            {
                throw new ArgumentNullException("mapCreators");
            }

            _configurationProvider = configurationProvider;
            _profileExpression = profileExpression;
            _mapCreators = mapCreators;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            foreach (IMapCreator mapCreator in _mapCreators)
            {
                mapCreator.CreateMap(_profileExpression);
            }

            _configurationProvider.AssertConfigurationIsValid();
        }

        public void Reset()
        {
            //NOTE: there isn't a need to reset the maps because IConfigurationProvider and IProfileExpression should be new instances on subsequent calls to Execute
        }

        #endregion
    }
}