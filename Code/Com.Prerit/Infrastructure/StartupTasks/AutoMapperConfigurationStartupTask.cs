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

        private readonly IMapCreator[] _mapCreators;

        #endregion

        #region Constructors

        public AutoMapperConfigurationStartupTask(IConfigurationProvider configurationProvider, IProfileExpression profileExpression, IMapCreator[] mapCreators)
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

            if (mapCreators.Length == 0)
            {
                throw new ArgumentException("Cannot be an empty array", "mapCreators");
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
            // NOTE: there currently is no way to remove maps
        }

        #endregion
    }
}